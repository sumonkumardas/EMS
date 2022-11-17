using System;
using System.Collections.Generic;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueBreakTimePropertyChecker
  {
    bool IsUniqueBreakTime(TimeSpan startTime, TimeSpan endTime, IEnumerable<WeekDays> weekDays, long branchMediumId);
    bool IsBreakTimeBetweenShiftTime(TimeSpan startTime, TimeSpan endTime, long branchMediumId);
    bool IsBreakTimeOverlaps(TimeSpan startTime, TimeSpan endTime, IEnumerable<WeekDays> weekDays, long branchMediumId);
  }
}