using Microsoft.AspNetCore.Identity;
using SinePulse.EMS.Domain.UserManagement;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueRolePropertyChecker : IUniqueRolePropertyChecker
  {
    private readonly RoleManager<Role> _roleManager;

    public UniqueRolePropertyChecker(RoleManager<Role> roleManager)
    {
      _roleManager = roleManager;
    }

    public bool IsUniqueRoleName(string roleName)
    {
       var result = _roleManager.FindByNameAsync(roleName).GetAwaiter().GetResult();
      return result == null;
    }
  }
}