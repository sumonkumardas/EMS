namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueRolePropertyChecker
  {
    bool IsUniqueRoleName(string roleName);
  }
}