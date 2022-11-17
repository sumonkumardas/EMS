using SinePulse.EMS.Messages.RoleMessages;

namespace SinePulse.EMS.UseCases.Roles
{
  public interface IAddRoleUseCase
  {
    AddRoleResponseMessage AddRole(AddRoleRequestMessage requestMessage);
  }
}