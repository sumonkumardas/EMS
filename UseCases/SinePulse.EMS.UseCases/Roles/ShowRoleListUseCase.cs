using System;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.RoleMessages;

namespace SinePulse.EMS.UseCases.Roles
{
  public class ShowRoleListUseCase : IShowRoleListUseCase
  {
    private readonly RoleManager<Role> _roleManager;

    public ShowRoleListUseCase(RoleManager<Role> roleManager)
    {
      _roleManager = roleManager;
    }

    public ShowRoleListResponseMessage ShowRoleList(ShowRoleListRequestMessage request)
    {
      Collection<ShowRoleListResponseMessage.Role> roles = new Collection<ShowRoleListResponseMessage.Role>();
      foreach (var role in _roleManager.Roles)
      {
        roles.Add(RoleEntity(role));
      }
      
      return new ShowRoleListResponseMessage(roles);
    }
    
    private ShowRoleListResponseMessage.Role RoleEntity(Role roleEntity)
    {
      return new ShowRoleListResponseMessage.Role
      {
        RoleId = roleEntity.Id,
        RoleName = roleEntity.Name,
        Status = ConvertToMessageStatus(StatusEnum.Active),
        RoleType = roleEntity.RoleType
      };
    }
    private static StatusEnum ConvertToMessageStatus(StatusEnum entityStatus)
    {
      switch (entityStatus)
      {
        case StatusEnum.Active:
          return StatusEnum.Active;
        case StatusEnum.Inactive:
          return StatusEnum.Inactive;
        case StatusEnum.Pending:
          return StatusEnum.Pending;
        case StatusEnum.Transferred:
          return StatusEnum.Transferred;
        case StatusEnum.Passed:
          return StatusEnum.Passed;
        default:
          throw new ArgumentOutOfRangeException(nameof(entityStatus), entityStatus, null);
      }
    }
  }
}