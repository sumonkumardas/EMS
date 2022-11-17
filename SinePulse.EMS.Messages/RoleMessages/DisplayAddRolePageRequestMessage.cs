using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.RoleMessages
{
  public class DisplayAddRolePageRequestMessage : IRequest<ValidatedData<DisplayAddRolePageResponseMessage>>
  {
  }
}