using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.AuthorizationMessages
{
  public class GetUserAssociationRequestMessage : IRequest<ValidatedData<GetUserAssociationResponseMessage>>
  {
    public string Username { get; set; }
  }
}