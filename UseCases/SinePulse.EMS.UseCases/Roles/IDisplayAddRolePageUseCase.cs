using SinePulse.EMS.Messages.RoleMessages;

namespace SinePulse.EMS.UseCases.Roles
{
  public interface IDisplayAddRolePageUseCase
  {
    DisplayAddRolePageResponseMessage DisplayAddRolePage(DisplayAddRolePageRequestMessage requestMessage);
  }
}