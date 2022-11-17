namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IAddLoginUserPropertyChecker
  {
    bool IsLoginUserAlreadyCreated(long employeeId);
  }
}