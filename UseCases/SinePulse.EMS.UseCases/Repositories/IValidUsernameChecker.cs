namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IValidUsernameChecker
  {
    bool IsValidUsername(string username);
  }
}