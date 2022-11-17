namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueWorkingShiftPropertyChecker
  {
    bool IsUniqueWorkingShiftName(string shiftName);
  }
}