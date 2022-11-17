namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IChangeLoginUserPasswordPropertyChecker
  {
    bool IsOldPasswordMatch(string oldPassword, string employeeCode);
    bool IsUserExists(string employeeCode);
  }
}