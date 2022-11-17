using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Transactions;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.FinancialJobService.Services
{
  public class DailyBalanceUpdater : IDailyBalanceUpdater
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public DailyBalanceUpdater(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public async Task UpdateDailyBalance(DateTime date, long accountHeadId)
    {
      var transactionEntries = _repository.Filter<TransactionEntry>(x =>
        x.AccountHead.Id == accountHeadId &&
        x.Transaction.TransactionDate == date);
      var balanceAmount = await transactionEntries.SumAsync(x => x.Amount);

      var dailyBalance = await _repository
        .Filter<DailyBalance>(x => x.Date == date && x.AccountHead.Id == accountHeadId)
        .FirstOrDefaultAsync();

      if (dailyBalance != null)
      {
        dailyBalance.BalanceAmount = balanceAmount;
      }
      else
      {
        _repository.Insert(new DailyBalance
        {
          Date = date,
          BalanceAmount = balanceAmount,
          AccountHeadId = accountHeadId
        });
      }
      
      await _dbContext.SaveChangesAsync();
    }
  }
}