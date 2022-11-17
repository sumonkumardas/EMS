using System;

namespace SinePulse.EMS.Utility
{
  public class TimeOverlappingChecker : ITimeOverlappingChecker
  {
    public bool DoesOverlap(TimeSpan startTime1, TimeSpan endTime1, TimeSpan startTime2, TimeSpan endTime2)
    {
      return InBetween(startTime1, endTime1, startTime2) ||
             InBetween(startTime1, endTime1, endTime2) ||
             InBetween(startTime2, endTime2, startTime1) ||
             InBetween(startTime2, endTime2, endTime1);
    }

    private bool InBetween(TimeSpan startTime, TimeSpan endTime, TimeSpan time)
    {
      return startTime < time && time < endTime;
    }
  }
}