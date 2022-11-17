using Microsoft.AspNetCore.Identity;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.Messages.RoleMessages;

namespace SinePulse.EMS.UseCases.Roles
{
  public class AddRoleUseCase : IAddRoleUseCase
  {
    private readonly RoleManager<Role> _roleManager;

    public AddRoleUseCase(RoleManager<Role> roleManager)
    {
      _roleManager = roleManager;
    }

    public AddRoleResponseMessage AddRole(AddRoleRequestMessage requestMessage)
    {
      var role = new Role();
      role.Name = requestMessage.RoleName;
      role.RoleType = requestMessage.RoleType;
      var result = _roleManager.CreateAsync(role).GetAwaiter().GetResult();
      return new AddRoleResponseMessage(result.Succeeded);
    }
  }
}