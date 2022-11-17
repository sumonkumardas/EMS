using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.RoleMessages
{
  public class ShowRoleListRequestMessage : IRequest<ValidatedData<ShowRoleListResponseMessage>>
  {
  }
}