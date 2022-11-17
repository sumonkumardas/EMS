using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Utility;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class ClassTestOverlappingChecker : IClassTestOverlappingChecker
  {
    private readonly EmsDbContext _dbContext;
    private readonly ITimestampOverlappingChecker _timestampOverlappingChecker;
    private const bool Overlaps = true;

    public ClassTestOverlappingChecker(EmsDbContext dbContext,
      ITimestampOverlappingChecker timestampOverlappingChecker)
    {
      _dbContext = dbContext;
      _timestampOverlappingChecker = timestampOverlappingChecker;
    }

    public bool IsNonOverlappingClassTestPeriod(DateTime startTimestamp, DateTime endTimestamp, long sectionId)
    {
      return !_dbContext.ClassTests
        .Where(t => t.Section != null && t.Section.Id == sectionId)
        .Any(t => DoesOverlap(t.StartTimestamp, t.EndTimestamp, startTimestamp, endTimestamp));
    }

    public bool IsNonOverlappingClassTestPeriod(DateTime startTimestamp, DateTime endTimestamp, long sectionId,long classTestId)
    {
      return !_dbContext.ClassTests
        .Where(t => t.Section != null && t.Section.Id == sectionId && t.Id!= classTestId)
        .Any(t => DoesOverlap(t.StartTimestamp, t.EndTimestamp, startTimestamp, endTimestamp));
    }

    public bool IsClassStartTimeBetweenShiftPeriod(TimeSpan startTime, long sectionId)
    {
      var section = _dbContext.Sections
        .Include(nameof(BranchMedium))
        .Include(nameof(BranchMedium) + "." + nameof(Shift))
        .FirstOrDefault(s => s.Id == sectionId);
      return section != null
             && section.BranchMedium.Shift.StartTime <= startTime
             && section.BranchMedium.Shift.EndTime > startTime;
    }

    public bool IsClassEndTimeBetweenShiftPeriod(TimeSpan endTime, long sectionId)
    {
      var section = _dbContext.Sections
        .Include(nameof(BranchMedium))
        .Include(nameof(BranchMedium) + "." + nameof(Shift))
        .FirstOrDefault(s => s.Id == sectionId);
      return section != null
             && section.BranchMedium.Shift.EndTime >= endTime
             && section.BranchMedium.Shift.StartTime < endTime;
    }

    public bool IsBreakTimeExist(long sectionId)
    {
      var branchMedium = _dbContext.Sections
        .Include(nameof(BranchMedium))
        .FirstOrDefault(s => s.Id == sectionId)?
        .BranchMedium;

      var breakTime = _dbContext.BreakTimes.FirstOrDefault(x => x.BranchMedium.Id == branchMedium.Id);
      return breakTime != null;
    }

    public bool IsTimeOverlapsWithBreakTime(TimeSpan startTime, TimeSpan endTime, long sectionId)
    {
      var branchMedium = _dbContext.Sections
        .Include(nameof(BranchMedium))
        .FirstOrDefault(s => s.Id == sectionId)?
        .BranchMedium;

      var breakTime = _dbContext.BreakTimes.FirstOrDefault(x => x.BranchMedium.Id == branchMedium.Id);
      return branchMedium != null && breakTime != null &&
             !DoesOverlap(breakTime.StartTime, breakTime.EndTime, startTime, endTime);
    }

    private bool DoesOverlap(DateTime startDate1, DateTime endDate1, DateTime startDate2, DateTime endDate2)
    {
      return _timestampOverlappingChecker.DoesOverlap(startDate1, endDate1, startDate2, endDate2);
    }

    private bool DoesOverlap(TimeSpan startTime1, TimeSpan endTime1, TimeSpan startTime2, TimeSpan endTime2)
    {
      if (startTime1 == startTime2 && endTime1 == endTime2)
        return Overlaps;
      return _timestampOverlappingChecker.DoesOverlap(startTime1, endTime1, startTime2, endTime2);
    }
  }
}