using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.RoleMessages
{
  public class ShowRoleRequestMessage : IRequest<ValidatedData<ShowRoleResponseMessage>>
  {
    public string RoleId { get; set; }
  }
}