using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Transactions;
using SinePulse.EMS.Messages.IncomeStatementMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.IncomeStatements
{
  public class ShowIncomeStatementUseCase : IShowIncomeStatementUseCase
  {
    private readonly IRepository _repository;

    public ShowIncomeStatementUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public ShowIncomeStatementResponseMessage ShowIncomeStatement(ShowIncomeStatementRequestMessage requestMessage)
    {
      var allAccountHeads = _repository.Filter<BranchMediumAccountHead, BranchMediumAccountHead, AccountType>(
        x => x.BranchMedium.Id == requestMessage.BranchMediumId,
        x => x.ParentHead,
        x => x.AccountType).ToList();
      var incomeDbAccountHead = allAccountHeads.FirstOrDefault(
        x => x.ParentHead == null && x.AccountType.AccountTypeName == ChartOfAccountTypeEnum.Revenue);
      var expenseDbAccountHead = allAccountHeads.FirstOrDefault(
        x => x.ParentHead == null && x.AccountType.AccountTypeName == ChartOfAccountTypeEnum.Expense);

      if (incomeDbAccountHead == null || expenseDbAccountHead == null) return null;

      var incomeAccountHead = CreateRequestHeadTree(incomeDbAccountHead, allAccountHeads, requestMessage.StartDate,
        requestMessage.EndDate);

      var expenseAccountHead = CreateRequestHeadTree(expenseDbAccountHead, allAccountHeads, requestMessage.StartDate,
        requestMessage.EndDate);

      return new ShowIncomeStatementResponseMessage
      {
        RevenueAccountHead = incomeAccountHead,
        ExpenseAccountHead = expenseAccountHead,
        TotalExpense = expenseAccountHead.Balance,
        TotalRevenue = incomeAccountHead.Balance,
        NetIncome = incomeAccountHead.Balance - expenseAccountHead.Balance
      };
    }

    private ShowIncomeStatementResponseMessage.AccountHead CreateRequestHeadTree(BranchMediumAccountHead dbAccountHead,
      List<BranchMediumAccountHead> allAccountHeads, DateTime startDate, DateTime endDate)
    {
      var nextLevelAccountHeads = allAccountHeads.Where(x => x.ParentHead?.Id == dbAccountHead.Id)
        .Select(x => CreateRequestHeadTree(x, allAccountHeads, startDate, endDate)).ToList();
      var balance = nextLevelAccountHeads.Any()
        ? nextLevelAccountHeads.Sum(x => x.Balance)
        : ReadBalance(startDate, endDate, dbAccountHead.Id);
      var transactionEntryType = GetTransactionEntryType(dbAccountHead.Id);
      var debit = transactionEntryType == AccountTransactionTypeEnum.Debit ? balance : 0;
      var credit = transactionEntryType == AccountTransactionTypeEnum.Credit ? balance : 0;

      return new ShowIncomeStatementResponseMessage.AccountHead
      {
        AccountHeadId = dbAccountHead.Id,
        AccountHeadName = dbAccountHead.AccountHeadName,
        Balance = balance,
        Debit = debit,
        Credit = credit,
        TransactionEntryType = transactionEntryType,
        AccountHeads = nextLevelAccountHeads
      };
    }

    private decimal ReadBalance(DateTime startDate, DateTime endDate, long accountHeadId)
    {
      return _repository.Filter<DailyBalance>(x =>
        x.AccountHead.Id == accountHeadId && startDate <= x.Date && x.Date <= endDate).Sum(x => x.BalanceAmount);
    }

    private AccountTransactionTypeEnum GetTransactionEntryType(long accountHeadId)
    {
      var accountsHead = _repository.GetById<BranchMediumAccountHead, AccountType>(
        accountHeadId, x => x.AccountType);
      return accountsHead.AccountType.TransactionType;
    }
  }
}