namespace SinePulse.EMS.Utility
{
  public class IntegerOverlappingChecker : IIntegerOverlappingChecker
  {
    public bool DoesOverlap(int startInteger1, int endInteger1, int startInteger2, int endInteger2)
    {
      return InBetween(startInteger1, endInteger1, startInteger2) ||
             InBetween(startInteger1, endInteger1, endInteger2) ||
             InBetween(startInteger2, endInteger2, startInteger1) ||
             InBetween(startInteger2, endInteger2, endInteger1);
    }

    private bool InBetween(int startInteger, int endInteger, int integer)
    {
      return startInteger < integer && integer < endInteger;
    }
  }
}