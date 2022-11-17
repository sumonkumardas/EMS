using System;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.AlertProcessingService.Utility
{
  public static class TimeSpanUtility
  {
    public static TimeSpan ToTimeSpan(int timeInterval, TimeIntervalType timeIntervalType)
    {
      switch (timeIntervalType)
      {
        case TimeIntervalType.Hour:
          return TimeSpan.FromHours(timeInterval);
        case TimeIntervalType.Minute:
          return TimeSpan.FromMinutes(timeInterval);
        case TimeIntervalType.Day:
          return TimeSpan.FromDays(timeInterval);
        default:
          throw new ArgumentOutOfRangeException(nameof(timeIntervalType), timeIntervalType, null);
      }
    }
  }
}