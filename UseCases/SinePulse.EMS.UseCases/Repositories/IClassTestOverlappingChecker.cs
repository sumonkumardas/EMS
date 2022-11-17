using System;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IClassTestOverlappingChecker
  {
    bool IsNonOverlappingClassTestPeriod(DateTime startTimestamp, DateTime endTimestamp, long sectionId);
    bool IsNonOverlappingClassTestPeriod(DateTime startTimestamp, DateTime endTimestamp, long sectionId,long classTestId);
    bool IsBreakTimeExist(long sectionId);
    bool IsClassStartTimeBetweenShiftPeriod(TimeSpan startTime, long sectionId);
    bool IsClassEndTimeBetweenShiftPeriod(TimeSpan endTime, long sectionId);
    bool IsTimeOverlapsWithBreakTime(TimeSpan startTime, TimeSpan endTime, long sectionId);
  }
}