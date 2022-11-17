using System;
using Xunit;

namespace SinePulse.EMS.Utility.Tests
{
  public class TimeOverlappingCheckerTest
  {
    private readonly TimeOverlappingChecker _timeOverlappingChecker;

    public TimeOverlappingCheckerTest()
    {
      _timeOverlappingChecker = new TimeOverlappingChecker();
    }

    [Fact]
    public void ShouldAbleToDetermineWhenTimesDoOverlapped()
    {
      Assert.True(_timeOverlappingChecker.DoesOverlap(
        new TimeSpan(8, 0, 0), new TimeSpan(10, 0, 0),
        new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0)));
      Assert.True(_timeOverlappingChecker.DoesOverlap(
        new TimeSpan(8, 0, 0), new TimeSpan(10, 0, 0),
        new TimeSpan(7, 0, 0), new TimeSpan(11, 0, 0)));
      Assert.True(_timeOverlappingChecker.DoesOverlap(
        new TimeSpan(8, 0, 0), new TimeSpan(10, 0, 0),
        new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 1)));
      Assert.True(_timeOverlappingChecker.DoesOverlap(
        new TimeSpan(8, 0, 0), new TimeSpan(10, 0, 0),
        new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 1)));
    }

    [Fact]
    public void ShouldAbleToDetermineWhenTimesDoNotOverlapped()
    {
      Assert.False(_timeOverlappingChecker.DoesOverlap(
        new TimeSpan(8, 0, 0), new TimeSpan(9, 0, 0),
        new TimeSpan(10, 0, 0), new TimeSpan(12, 0, 0)));
      Assert.False(_timeOverlappingChecker.DoesOverlap(
        new TimeSpan(11, 0, 0), new TimeSpan(10, 0, 0),
        new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0)));
      Assert.False(_timeOverlappingChecker.DoesOverlap(
        new TimeSpan(8, 0, 0), new TimeSpan(10, 0, 0),
        new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0)));
      Assert.False(_timeOverlappingChecker.DoesOverlap(
        new TimeSpan(8, 0, 0), new TimeSpan(10, 0, 0),
        new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0)));
    }
  }
}