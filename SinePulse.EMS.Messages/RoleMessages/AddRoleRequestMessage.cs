using MediatR;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Messages.RoleMessages
{
  public class AddRoleRequestMessage : IRequest<ValidatedData<AddRoleResponseMessage>>
  {
    public string RoleName { get; set; }
    public string CurrentUserName { get; set; }
    public RoleTypeEnum RoleType { get; set; }
  }
}