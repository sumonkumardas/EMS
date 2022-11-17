using SinePulse.EMS.Messages.RoleMessages;

namespace SinePulse.EMS.UseCases.Roles
{
  public interface IShowRoleListUseCase
  {
    ShowRoleListResponseMessage ShowRoleList(ShowRoleListRequestMessage request);
  }
}