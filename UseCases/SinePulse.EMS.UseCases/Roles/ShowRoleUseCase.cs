using System;
using Microsoft.AspNetCore.Identity;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.Messages.RoleMessages;

namespace SinePulse.EMS.UseCases.Roles
{
  public class ShowRoleUseCase : IShowRoleUseCase
  {
    private readonly RoleManager<Role> _roleManager;

    public ShowRoleUseCase(RoleManager<Role> roleManager)
    {
      _roleManager = roleManager;
    }

    public ShowRoleResponseMessage ShowRole(ShowRoleRequestMessage message)
    {
      var identityRole = _roleManager.FindByIdAsync(message.RoleId).GetAwaiter().GetResult();
      return new ShowRoleResponseMessage(RoleEntity(identityRole));
    }

    private ShowRoleResponseMessage.Role RoleEntity(Role roleEntity)
    {
      return new ShowRoleResponseMessage.Role
      {
        RoleId = roleEntity.Id,
        RoleName = roleEntity.Name,
        Status = ConvertToMessageStatus(StatusEnum.Active),
        RoleType = roleEntity.RoleType
      };
    }

    private static Messages.Model.Enums.StatusEnum ConvertToMessageStatus(StatusEnum entityStatus)
    {
      switch (entityStatus)
      {
        case StatusEnum.Active:
          return Messages.Model.Enums.StatusEnum.Active;
        case StatusEnum.Inactive:
          return Messages.Model.Enums.StatusEnum.Inactive;
        case StatusEnum.Pending:
          return Messages.Model.Enums.StatusEnum.Pending;
        case StatusEnum.Transferred:
          return Messages.Model.Enums.StatusEnum.Transferred;
        case StatusEnum.Passed:
          return Messages.Model.Enums.StatusEnum.Passed;
        default:
          throw new ArgumentOutOfRangeException(nameof(entityStatus), entityStatus, null);
      }
    }
  }
}