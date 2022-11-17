using System;
using Xunit;

namespace SinePulse.EMS.Utility.Tests
{
  public class TimestampOverlappingCheckerTest
  {
    private readonly TimestampOverlappingChecker _timestampOverlappingChecker;

    public TimestampOverlappingCheckerTest()
    {
      _timestampOverlappingChecker = new TimestampOverlappingChecker();
    }

    [Fact]
    public void ShouldAbleToDetermineWhenTimestampsDoOverlapped()
    {
      Assert.True(_timestampOverlappingChecker.DoesOverlap(
        new DateTime(2000, 7, 25, 8, 0, 0), new DateTime(2000, 7, 25, 10, 0, 0),
        new DateTime(2000, 7, 25, 9, 0, 0), new DateTime(2000, 7, 25, 12, 0, 0)));
      Assert.True(_timestampOverlappingChecker.DoesOverlap(
        new DateTime(1987, 4, 3, 8, 0, 0), new DateTime(1987, 4, 3, 10, 0, 0),
        new DateTime(1987, 4, 3, 7, 0, 0), new DateTime(1987, 4, 3, 11, 0, 0)));
      Assert.True(_timestampOverlappingChecker.DoesOverlap(
        new DateTime(2070, 9, 13, 8, 0, 0), new DateTime(2070, 9, 13, 10, 0, 0),
        new DateTime(2070, 9, 13, 7, 0, 0), new DateTime(2070, 9, 13, 8, 0, 1)));
      Assert.True(_timestampOverlappingChecker.DoesOverlap(
        new DateTime(2019, 11, 6, 8, 0, 0), new DateTime(2019, 11, 6, 10, 0, 0),
        new DateTime(2019, 11, 6, 7, 0, 0), new DateTime(2019, 11, 6, 8, 0, 1)));
      Assert.True(_timestampOverlappingChecker.DoesOverlap(
        new DateTime(2018, 12, 31, 20, 0, 0), new DateTime(2019, 1, 1, 1, 0, 0),
        new DateTime(2018, 12, 31, 23, 0, 0), new DateTime(2019, 1, 1, 0, 30, 0)));
    }

    [Fact]
    public void ShouldAbleToDetermineWhenTimestampsDoNotOverlapped()
    {
      Assert.False(_timestampOverlappingChecker.DoesOverlap(
        new DateTime(2000, 7, 25, 8, 0, 0), new DateTime(2000, 7, 25, 9, 0, 0),
        new DateTime(2000, 7, 25, 10, 0, 0), new DateTime(2000, 7, 25, 12, 0, 0)));
      Assert.False(_timestampOverlappingChecker.DoesOverlap(
        new DateTime(1987, 4, 3, 11, 0, 0), new DateTime(1987, 4, 3, 10, 0, 0),
        new DateTime(1987, 4, 3, 7, 0, 0), new DateTime(1987, 4, 3, 8, 0, 0)));
      Assert.False(_timestampOverlappingChecker.DoesOverlap(
        new DateTime(2070, 9, 13, 8, 0, 0), new DateTime(2070, 9, 13, 10, 0, 0),
        new DateTime(2070, 9, 14, 7, 0, 0), new DateTime(2070, 9, 14, 8, 0, 0)));
      Assert.False(_timestampOverlappingChecker.DoesOverlap(
        new DateTime(2019, 11, 6, 8, 0, 0), new DateTime(2019, 11, 6, 10, 0, 0),
        new DateTime(2019, 11, 6, 7, 0, 0), new DateTime(2019, 11, 6, 8, 0, 0)));
      Assert.False(_timestampOverlappingChecker.DoesOverlap(
        new DateTime(2018, 12, 31, 20, 0, 0), new DateTime(2019, 1, 1, 1, 0, 0),
        new DateTime(2018, 12, 30, 23, 0, 0), new DateTime(2018, 12, 31, 0, 30, 0)));
    }
  }
}