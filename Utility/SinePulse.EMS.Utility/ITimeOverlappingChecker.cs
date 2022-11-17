using System;

namespace SinePulse.EMS.Utility
{
  public interface ITimeOverlappingChecker
  {
    bool DoesOverlap(TimeSpan startTime1, TimeSpan endTime1, TimeSpan startTime2, TimeSpan endTime2);
  }
}