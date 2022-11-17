using System;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface ISessionOverlappingChecker
  {
    bool IsNonOverlappingGlobalSessionPeriod(DateTime startDate, DateTime endDate);
    bool IsNonOverlappingInstituteSpecificSessionPeriod(DateTime startDate, DateTime endDate, long instituteId);
    bool IsNonOverlappingBranchSpecificSessionPeriod(DateTime startDate, DateTime endDate, long branchId);
    bool IsNonOverlappingMediumSpecificSessionPeriod(DateTime startDate, DateTime endDate, long branchMediumId);
    bool IsNonOverlappingBranchMediumSpecificSessionPeriod(DateTime sessionStartDate, DateTime sessionEndDate, long sessionObjectId);
  }
}