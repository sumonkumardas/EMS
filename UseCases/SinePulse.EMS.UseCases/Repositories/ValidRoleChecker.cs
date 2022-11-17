using Microsoft.AspNetCore.Identity;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class ValidRoleChecker : IValidRoleChecker
  {
    private readonly EmsDbContext _dbContext;
    private readonly RoleManager<Role> _roleManager;

    public ValidRoleChecker(EmsDbContext dbContext, RoleManager<Role> roleManager)
    {
      _dbContext = dbContext;
      _roleManager = roleManager;
    }

    public bool IsValidRole(string roleId)
    {
      var identityRole = _roleManager.FindByIdAsync(roleId).GetAwaiter().GetResult();
      return identityRole != null;
    }
  }
}