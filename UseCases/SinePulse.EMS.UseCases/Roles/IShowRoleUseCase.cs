using SinePulse.EMS.Messages.RoleMessages;

namespace SinePulse.EMS.UseCases.Roles
{
  public interface IShowRoleUseCase
  {
    ShowRoleResponseMessage ShowRole(ShowRoleRequestMessage message);
  }
}