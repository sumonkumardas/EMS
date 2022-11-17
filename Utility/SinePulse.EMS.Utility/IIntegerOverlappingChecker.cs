namespace SinePulse.EMS.Utility
{
  public interface IIntegerOverlappingChecker
  {
    bool DoesOverlap(int startInteger1, int endInteger1, int startInteger2, int endInteger2);
  }
}