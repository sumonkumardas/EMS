using MediatR;

namespace SinePulse.EMS.Messages.ServiceChargeMessages
{
  public class GetServiceChargeRequestMessage : IRequest<GetServiceChargeResponseMessage>
  {
    public long BranchMediumId { get; set; }
  }
}
