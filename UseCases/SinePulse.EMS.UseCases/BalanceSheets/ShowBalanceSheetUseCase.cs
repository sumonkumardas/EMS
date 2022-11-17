using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Transactions;
using SinePulse.EMS.Messages.BalanceSheetMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.UseCases.Sessions;

namespace SinePulse.EMS.UseCases.BalanceSheets
{
  public class ShowBalanceSheetUseCase : IShowBalanceSheetUseCase
  {
    private readonly IRepository _repository;
    private readonly ICurrentSessionProvider _currentSessionProvider;

    public ShowBalanceSheetUseCase(IRepository repository, ICurrentSessionProvider currentSessionProvider)
    {
      _repository = repository;
      _currentSessionProvider = currentSessionProvider;
    }

    public ShowBalanceSheetResponseMessage ShowBalanceSheet(ShowBalanceSheetRequestMessage requestMessage)
    {
      var session = _currentSessionProvider.GetCurrentSession(requestMessage.BranchMediumId);
      var allAccountHeads = _repository.Filter<BranchMediumAccountHead, BranchMediumAccountHead, AccountType>(
        x => x.BranchMedium.Id == requestMessage.BranchMediumId,
        x => x.ParentHead,
        x => x.AccountType).ToList();
      var assetDbAccountHead = allAccountHeads.FirstOrDefault(
        x => x.ParentHead == null && x.AccountType.AccountTypeName == ChartOfAccountTypeEnum.Assets);
      var liabilityDbAccountHead = allAccountHeads.FirstOrDefault(
        x => x.ParentHead == null && x.AccountType.AccountTypeName == ChartOfAccountTypeEnum.Liabilities);
      var equityDbAccountHead = allAccountHeads.FirstOrDefault(
        x => x.ParentHead == null && x.AccountType.AccountTypeName == ChartOfAccountTypeEnum.Equity);
      var revenueDbAccountHead = allAccountHeads.FirstOrDefault(
        x => x.ParentHead == null && x.AccountType.AccountTypeName == ChartOfAccountTypeEnum.Revenue);
      var expenseDbAccountHead = allAccountHeads.FirstOrDefault(
        x => x.ParentHead == null && x.AccountType.AccountTypeName == ChartOfAccountTypeEnum.Expense);

      if (assetDbAccountHead == null || liabilityDbAccountHead == null || equityDbAccountHead == null ||
          revenueDbAccountHead == null || expenseDbAccountHead == null) return null;

      var assetAccountHead = CreateRequestHeadTree(assetDbAccountHead, allAccountHeads,
        requestMessage.StartDate, requestMessage.EndDate);

      var liabilityAccountHead = CreateRequestHeadTree(liabilityDbAccountHead, allAccountHeads,
        requestMessage.StartDate, requestMessage.EndDate);

      var equityAccountHead = CreateRequestHeadTree(equityDbAccountHead, allAccountHeads,
        requestMessage.StartDate, requestMessage.EndDate);

      var incomeAccountHead = CreateRequestHeadTree(revenueDbAccountHead, allAccountHeads,
        requestMessage.StartDate, requestMessage.EndDate);

      var expenseAccountHead = CreateRequestHeadTree(expenseDbAccountHead, allAccountHeads,
        requestMessage.StartDate, requestMessage.EndDate);

      return new ShowBalanceSheetResponseMessage
      {
        AssetAccountHead = assetAccountHead,
        LiabilitiesAccountHead = liabilityAccountHead,
        EquityAccountHead = equityAccountHead,
        TotalAsset = assetAccountHead.Amount,
        TotalLiabilities = liabilityAccountHead.Amount,
        TotalEquity = equityAccountHead.Amount,
        NetIncome = incomeAccountHead.Amount - expenseAccountHead.Amount,
        IsYearClosing = session.IsSessionClosing || session.IsSessionClosed 
      };
    }

    private ShowBalanceSheetResponseMessage.AccountHead CreateRequestHeadTree(BranchMediumAccountHead dbAccountHead,
      List<BranchMediumAccountHead> allAccountHeads, DateTime startDate, DateTime endDate)
    {
      var nextLevelAccountHeads = allAccountHeads.Where(x => x.ParentHead?.Id == dbAccountHead.Id)
        .Select(x => CreateRequestHeadTree(x, allAccountHeads, startDate, endDate)).ToList();
      var amount = nextLevelAccountHeads.Any()
        ? nextLevelAccountHeads.Sum(x => x.Amount)
        : ReadBalance(startDate, endDate, dbAccountHead.Id);
      var transactionEntryType = GetTransactionEntryType(dbAccountHead.Id);

      return new ShowBalanceSheetResponseMessage.AccountHead
      {
        AccountHeadId = dbAccountHead.Id,
        AccountHeadName = dbAccountHead.AccountHeadName,
        Amount = amount,
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