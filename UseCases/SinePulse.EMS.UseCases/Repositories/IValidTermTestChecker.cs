namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IValidTermTestChecker
  {
    bool IsValidTermTest(long testId);
  }
}