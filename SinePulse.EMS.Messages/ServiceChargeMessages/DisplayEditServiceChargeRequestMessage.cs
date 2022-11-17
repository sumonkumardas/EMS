using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.ServiceChargeMessages
{
  public class DisplayEditServiceChargeRequestMessage : IRequest<ValidatedData<DisplayEditServiceChargeResponseMessage>>
  {
    public long ServiceChargeId { get; set; }
    public long BranchMediumId { get; set; }
  }
}
