using System;
using System.Threading.Tasks;

namespace SinePulse.EMS.FinancialJobService.Services
{
  public interface IDailyBalanceUpdater
  {
    Task UpdateDailyBalance(DateTime date, long accountHeadId);
  }
}