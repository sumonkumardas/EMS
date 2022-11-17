using System;
using System.Linq;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Utility;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class SessionOverlappingChecker : ISessionOverlappingChecker
  {
    private readonly EmsDbContext _dbContext;
    private readonly ITimestampOverlappingChecker _timestampOverlappingChecker;

    public SessionOverlappingChecker(EmsDbContext dbContext, ITimestampOverlappingChecker timestampOverlappingChecker)
    {
      _dbContext = dbContext;
      _timestampOverlappingChecker = timestampOverlappingChecker;
    }

    public bool IsNonOverlappingGlobalSessionPeriod(DateTime startDate, DateTime endDate)
    {
      return !_dbContext.Sessions
        .Where(s => s.SessionType == ObjectTypeEnum.Global)
        .Any(s => DoesOverlap(s.StartDate, s.EndDate, startDate, endDate));
    }

    public bool IsNonOverlappingInstituteSpecificSessionPeriod(DateTime startDate, DateTime endDate, long instituteId)
    {
      return !_dbContext.Sessions
        .Where(s => s.SessionType == ObjectTypeEnum.Institute && s.Institute.Id == instituteId)
        .Any(s => DoesOverlap(s.StartDate, s.EndDate, startDate, endDate));
    }

    public bool IsNonOverlappingBranchSpecificSessionPeriod(DateTime startDate, DateTime endDate, long branchId)
    {
      return !_dbContext.Sessions
        .Where(s => s.SessionType == ObjectTypeEnum.Branch && s.Branch.Id == branchId)
        .Any(s => DoesOverlap(s.StartDate, s.EndDate, startDate, endDate));
    }

    public bool IsNonOverlappingMediumSpecificSessionPeriod(DateTime startDate, DateTime endDate, long branchMediumId)
    {
      return !_dbContext.Sessions
        .Where(s => s.SessionType == ObjectTypeEnum.Medium && s.BranchMedium.Id == branchMediumId)
        .Any(s => DoesOverlap(s.StartDate, s.EndDate, startDate, endDate));
    }
    
    public bool IsNonOverlappingBranchMediumSpecificSessionPeriod(DateTime startDate, DateTime endDate, long branchMediumId)
    {
      return !_dbContext.Sessions
        .Where(s => s.SessionType == ObjectTypeEnum.BranchMedium && s.BranchMedium.Id == branchMediumId)
        .Any(s => DoesOverlap(s.StartDate, s.EndDate, startDate, endDate));
    }


    private bool DoesOverlap(DateTime startDate1, DateTime endDate1, DateTime startDate2, DateTime endDate2)
    {
      const bool overlaps = true;
      if (startDate1 == startDate2 && endDate1 == endDate2)
        return overlaps;
      return _timestampOverlappingChecker.DoesOverlap(startDate1, endDate1, startDate2, endDate2);
    }
  }
}