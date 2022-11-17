using System;

namespace SinePulse.EMS.Utility
{
  public class TimestampOverlappingChecker : ITimestampOverlappingChecker
  {
    public bool DoesOverlap(
      DateTime startTimestamp1, DateTime endTimestamp1,
      DateTime startTimestamp2, DateTime endTimestamp2)
    {
      return InBetween(startTimestamp1, endTimestamp1, startTimestamp2) ||
             InBetween(startTimestamp1, endTimestamp1, endTimestamp2) ||
             InBetween(startTimestamp2, endTimestamp2, startTimestamp1) ||
             InBetween(startTimestamp2, endTimestamp2, endTimestamp1);
    }

    private bool InBetween(DateTime startTimestamp, DateTime endTimestamp, DateTime timestamp)
    {
      return startTimestamp <= timestamp && timestamp <= endTimestamp;
    }

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