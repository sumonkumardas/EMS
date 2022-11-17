using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Transactions;
using SinePulse.EMS.FinancialJobService.Data;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.FinancialJobService.Services
{
  public class YearClosingDataProvider : IYearClosingDataProvider
  {
    private readonly IRepository _repository;

    public YearClosingDataProvider(IRepository repository)
    {
      _repository = repository;
    }

    public async Task<RetainedEarningsData> GetRetainedEarningsData(
      ICollection<BranchMediumAccountHead> allAccountHeads, DateTime startDate, DateTime endDate)
    {
      var revenueDbAccountHead = allAccountHeads.First(
        x => x.ParentHead == null && x.AccountType.AccountTypeName == ChartOfAccountTypeEnum.Revenue);
      var expenseDbAccountHead = allAccountHeads.First(
        x => x.ParentHead == null && x.AccountType.AccountTypeName == ChartOfAccountTypeEnum.Expense);

      var totalRevenue =
        await ReadTotalBalanceOfAccountHead(revenueDbAccountHead, allAccountHeads, startDate, endDate);

      var totalExpense =
        await ReadTotalBalanceOfAccountHead(expenseDbAccountHead, allAccountHeads, startDate, endDate);

      return new RetainedEarningsData {TotalRevenue = totalRevenue, TotalExpense = totalExpense};
    }

    private async Task<decimal> ReadTotalBalanceOfAccountHead(BranchMediumAccountHead dbAccountHead,
      ICollection<BranchMediumAccountHead> allAccountHeads, DateTime startDate, DateTime endDate)
    {
      var nextLevelAccountHeads = GetNextLevelAccountHeads(allAccountHeads, dbAccountHead.Id)
        .Select(x => ReadTotalBalanceOfAccountHead(x, allAccountHeads, startDate, endDate)).ToList();
      var amount = nextLevelAccountHeads.Any()
        ? nextLevelAccountHeads.Sum(x => x.Result)
        : await ReadBalance(startDate, endDate, dbAccountHead.Id);

      return amount;
    }

    public async Task<OpeningBalanceData> GetOpeningBalanceData(ICollection<BranchMediumAccountHead> allAccountHeads,
      DateTime startDate, DateTime endDate)
    {
      var assetDbAccountHead = allAccountHeads.First(
        x => x.ParentHead == null && x.AccountType.AccountTypeName == ChartOfAccountTypeEnum.Assets);
      var liabilityDbAccountHead = allAccountHeads.First(
        x => x.ParentHead == null && x.AccountType.AccountTypeName == ChartOfAccountTypeEnum.Liabilities);
      var equityDbAccountHead = allAccountHeads.First(
        x => x.ParentHead == null && x.AccountType.AccountTypeName == ChartOfAccountTypeEnum.Equity);

      var assetAccountHeadBalances =
        await ReadAllLeafAccountHeadBalance(assetDbAccountHead, allAccountHeads, startDate, endDate);

      var liabilityAccountHeadBalances =
        await ReadAllLeafAccountHeadBalance(liabilityDbAccountHead, allAccountHeads, startDate, endDate);

      var equityAccountHeadBalances =
        await ReadAllLeafAccountHeadBalance(equityDbAccountHead, allAccountHeads, startDate, endDate);

      return new OpeningBalanceData
      {
        AssetAccountCodeBalances = assetAccountHeadBalances,
        LiabilityAccountCodeBalances = liabilityAccountHeadBalances,
        EquityAccountCodeBalances = equityAccountHeadBalances
      };
    }

    private async Task<ICollection<OpeningBalanceData.AccountCodeBalance>> ReadAllLeafAccountHeadBalance(
      BranchMediumAccountHead dbAccountHead, ICollection<BranchMediumAccountHead> allAccountHeads,
      DateTime startDate, DateTime endDate)
    {
      var subAccountCodeBalances = GetNextLevelAccountHeads(allAccountHeads, dbAccountHead.Id)
        .Select(x => ReadAllLeafAccountHeadBalance(x, allAccountHeads, startDate, endDate)).ToList();

      var accountCodeBalances = new List<OpeningBalanceData.AccountCodeBalance>();
      if (subAccountCodeBalances.Any())
      {
        subAccountCodeBalances.ForEach(async x => accountCodeBalances.AddRange(await x));
      }
      else
      {
        accountCodeBalances.Add(new OpeningBalanceData.AccountCodeBalance
        {
          AccountCode = dbAccountHead.AccountCode,
          Balance = await ReadBalance(startDate, endDate, dbAccountHead.Id)
        });
      }

      return accountCodeBalances;
    }

    private async Task<decimal> ReadBalance(DateTime startDate, DateTime endDate, long accountHeadId)
    {
      return await _repository.Filter<DailyBalance>(x =>
          x.AccountHead.Id == accountHeadId && startDate <= x.Date && x.Date <= endDate)
        .SumAsync(x => x.BalanceAmount);
    }

    private static IEnumerable<BranchMediumAccountHead> GetNextLevelAccountHeads(
      IEnumerable<BranchMediumAccountHead> allAccountHeads, long accountHeadId)
    {
      return allAccountHeads.Where(x => x.ParentHead?.Id == accountHeadId);
    }
  }
}