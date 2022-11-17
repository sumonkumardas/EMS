namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IValidRoleChecker
  {
    bool IsValidRole(string roleId);
  }
}