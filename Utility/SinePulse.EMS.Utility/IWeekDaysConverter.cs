using System;
using System.Collections.Generic;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Utility
{
  public interface IWeekDaysConverter
  {
    ICollection<DayOfWeek> ConvertToDaysOfWeek(WeekDays weekDays);
    WeekDays ConvertToWeekDays(ICollection<DayOfWeek> daysOfWeek);
    IEnumerable<WeekDays> ConvertToDaysOfWeekList(WeekDays weekDays);
  }
}