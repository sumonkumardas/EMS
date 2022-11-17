using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Transactions;
using SinePulse.EMS.Messages.TrialBalanceMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.TrialBalances
{
  public class ShowTrialBalanceUseCase : IShowTrialBalanceUseCase
  {
    private readonly IRepository _repository;

    public ShowTrialBalanceUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public ShowTrialBalanceResponseMessage ShowTrialBalance(ShowTrialBalanceRequestMessage requestMessage)
    {
      var allAccountHeads = _repository.Filter<BranchMediumAccountHead, BranchMediumAccountHead>(x =>
          x.BranchMedium.Id == requestMessage.BranchMediumId,
        x => x.ParentHead).ToList();

      if (!allAccountHeads.Any()) return null;
      
      var topLevelAccountHeads = allAccountHeads.Where(x => x.ParentHead == null);

      var accountHeads = topLevelAccountHeads.Select(x =>
        CreateRequestHeadTree(x, allAccountHeads, requestMessage.StartDate, requestMessage.EndDate)).ToList();
      return new ShowTrialBalanceResponseMessage
      {
        AccountHeads = accountHeads,
        TotalDebit = accountHeads.Sum(x => x.Debit),
        TotalCredit = accountHeads.Sum(x => x.Credit)
      };
    }

    private ShowTrialBalanceResponseMessage.AccountHead CreateRequestHeadTree(BranchMediumAccountHead dbAccountHead,
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

      return new ShowTrialBalanceResponseMessage.AccountHead
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
      if(accountsHead.AccountType.AccountTypeName == ChartOfAccountTypeEnum.Expense)
      {

      }
      return accountsHead.AccountType.TransactionType;
    }
  }
}