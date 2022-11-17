using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.FinancialJobService.Data;

namespace SinePulse.EMS.FinancialJobService.Services
{
  public interface IYearClosingDataProvider
  {
    Task<RetainedEarningsData> GetRetainedEarningsData(ICollection<BranchMediumAccountHead> allAccountHeads,
      DateTime startDate, DateTime endDate);

    Task<OpeningBalanceData> GetOpeningBalanceData(ICollection<BranchMediumAccountHead> allAccountHeads,
      DateTime startDate, DateTime endDate);
  }
}