using System;
using System.Collections.Generic;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Utility
{
  public static class WeekDaysExtensions
  {
    public static ICollection<DayOfWeek> ConvertToDaysOfWeek(this WeekDays weekDays)
    {
      var weekDaysConverter = new WeekDaysConverter();
      return weekDaysConverter.ConvertToDaysOfWeek(weekDays);
    }
  }
}