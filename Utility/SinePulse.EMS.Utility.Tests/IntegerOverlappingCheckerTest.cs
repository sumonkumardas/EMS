using Xunit;

namespace SinePulse.EMS.Utility.Tests
{
  public class IntegerOverlappingCheckerTest
  {
    private readonly IntegerOverlappingChecker _integerOverlappingChecker;

    public IntegerOverlappingCheckerTest()
    {
      _integerOverlappingChecker = new IntegerOverlappingChecker();
    }

    [Fact]
    public void ShouldAbleToDetermineWhenIntegersDoOverlapped()
    {
      Assert.True(_integerOverlappingChecker.DoesOverlap(8, 10, 9, 12));
      Assert.True(_integerOverlappingChecker.DoesOverlap(8, 10, 7, 11));
    }

    [Fact]
    public void ShouldAbleToDetermineWhenIntegersDoNotOverlapped()
    {
      Assert.False(_integerOverlappingChecker.DoesOverlap(8, 9, 10, 12));
      Assert.False(_integerOverlappingChecker.DoesOverlap(11, 10, 7, 8));
      Assert.False(_integerOverlappingChecker.DoesOverlap(8, 10, 7, 8));
    }
  }
}