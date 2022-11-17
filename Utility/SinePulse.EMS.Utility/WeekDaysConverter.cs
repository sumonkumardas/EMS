using System;
using System.Collections.Generic;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Utility
{
  public class WeekDaysConverter : IWeekDaysConverter
  {
    public ICollection<DayOfWeek> ConvertToDaysOfWeek(WeekDays weekDays)
    {
      var daysOfWeek = new List<DayOfWeek>();

      if (weekDays.HasFlag(WeekDays.Saturday))
        daysOfWeek.Add(DayOfWeek.Saturday);
      if (weekDays.HasFlag(WeekDays.Sunday))
        daysOfWeek.Add(DayOfWeek.Sunday);
      if (weekDays.HasFlag(WeekDays.Monday))
        daysOfWeek.Add(DayOfWeek.Monday);
      if (weekDays.HasFlag(WeekDays.Tuesday))
        daysOfWeek.Add(DayOfWeek.Tuesday);
      if (weekDays.HasFlag(WeekDays.Wednesday))
        daysOfWeek.Add(DayOfWeek.Wednesday);
      if (weekDays.HasFlag(WeekDays.Thursday))
        daysOfWeek.Add(DayOfWeek.Thursday);
      if (weekDays.HasFlag(WeekDays.Friday))
        daysOfWeek.Add(DayOfWeek.Friday);

      return daysOfWeek;
    }

    public WeekDays ConvertToWeekDays(ICollection<DayOfWeek> daysOfWeek)
    {
      var weekDays = WeekDays.None;

      foreach (var dayOfWeek in daysOfWeek)
      {
        switch (dayOfWeek)
        {
          case DayOfWeek.Saturday:
            weekDays |= WeekDays.Saturday;
            break;
          case DayOfWeek.Sunday:
            weekDays |= WeekDays.Sunday;
            break;
          case DayOfWeek.Monday:
            weekDays |= WeekDays.Monday;
            break;
          case DayOfWeek.Tuesday:
            weekDays |= WeekDays.Tuesday;
            break;
          case DayOfWeek.Wednesday:
            weekDays |= WeekDays.Wednesday;
            break;
          case DayOfWeek.Thursday:
            weekDays |= WeekDays.Thursday;
            break;
          case DayOfWeek.Friday:
            weekDays |= WeekDays.Friday;
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }

      return weekDays;
    }

    public IEnumerable<WeekDays> ConvertToDaysOfWeekList(WeekDays weekDays)
    {
      var weekDaysList = new List<WeekDays>();

      if (weekDays.HasFlag(WeekDays.Saturday))
        weekDaysList.Add(WeekDays.Saturday);
      if (weekDays.HasFlag(WeekDays.Sunday))
        weekDaysList.Add(WeekDays.Sunday);
      if (weekDays.HasFlag(WeekDays.Monday))
        weekDaysList.Add(WeekDays.Monday);
      if (weekDays.HasFlag(WeekDays.Tuesday))
        weekDaysList.Add(WeekDays.Tuesday);
      if (weekDays.HasFlag(WeekDays.Wednesday))
        weekDaysList.Add(WeekDays.Wednesday);
      if (weekDays.HasFlag(WeekDays.Thursday))
        weekDaysList.Add(WeekDays.Thursday);
      if (weekDays.HasFlag(WeekDays.Friday))
        weekDaysList.Add(WeekDays.Friday);

      return weekDaysList;
    }
  }
}