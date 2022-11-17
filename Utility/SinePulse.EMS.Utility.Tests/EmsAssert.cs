using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SinePulse.EMS.Utility.Tests
{
  public static class EmsAssert
  {
    public static void ContainsSameItems<TItem>(IEnumerable<TItem> expectedItems, IEnumerable<TItem> actualItems)
    {
      Assert.True(expectedItems.OrderBy(_ => _).SequenceEqual(actualItems.OrderBy(_ => _)));
    }
  }
}