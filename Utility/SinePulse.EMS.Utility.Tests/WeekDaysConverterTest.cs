using System;
using System.Collections.Generic;
using SinePulse.EMS.ProjectPrimitives;
using Xunit;

namespace SinePulse.EMS.Utility.Tests
{
  public class WeekDaysConverterTest
  {
    private readonly WeekDaysConverter _weekDaysConverter = new WeekDaysConverter();

    [Fact]
    public void CanCovertBetweenWeekDaysAndDaysOfWeek()
    {
      AssertCanCovertBetweenWeekDaysAndDaysOfWeek(WeekDays.None,
        new DayOfWeek[] { });
      AssertCanCovertBetweenWeekDaysAndDaysOfWeek(WeekDays.Friday | WeekDays.Saturday | WeekDays.Sunday,
        new[] {DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday});
      AssertCanCovertBetweenWeekDaysAndDaysOfWeek(WeekDays.Friday | WeekDays.Saturday,
        new[] {DayOfWeek.Friday, DayOfWeek.Saturday});
      AssertCanCovertBetweenWeekDaysAndDaysOfWeek(WeekDays.Friday,
        new[] {DayOfWeek.Friday});
      AssertCanCovertBetweenWeekDaysAndDaysOfWeek(WeekDays.Saturday | WeekDays.Sunday | WeekDays.Tuesday,
        new[] {DayOfWeek.Sunday, DayOfWeek.Tuesday, DayOfWeek.Saturday});
    }

    private void AssertCanCovertBetweenWeekDaysAndDaysOfWeek(WeekDays weekDays, ICollection<DayOfWeek> daysOfWeek)
    {
      var convertedDaysOfWeek = _weekDaysConverter.ConvertToDaysOfWeek(weekDays);
      EmsAssert.ContainsSameItems(daysOfWeek, convertedDaysOfWeek);

      var extensionMethodConvertedDaysOfWeek = weekDays.ConvertToDaysOfWeek();
      EmsAssert.ContainsSameItems(daysOfWeek, extensionMethodConvertedDaysOfWeek);

      var convertedWeekDays = _weekDaysConverter.ConvertToWeekDays(daysOfWeek);
      Assert.Equal(weekDays, convertedWeekDays);
    }
  }
}