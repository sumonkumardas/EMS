namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IShiftExistanceChecker
  {
    bool IsShiftExists(long shiftId);
  }
}