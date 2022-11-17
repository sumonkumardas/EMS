using System;

namespace SinePulse.EMS.Utility
{
  public interface ITimestampOverlappingChecker
  {
    bool DoesOverlap(
      DateTime startTimestamp1, DateTime endTimestamp1,
      DateTime startTimestamp2, DateTime endTimestamp2);

    bool DoesOverlap(TimeSpan startTime1, TimeSpan endTime1, TimeSpan startTime2, TimeSpan endTime2);
  }
}