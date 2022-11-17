using MediatR;

namespace SinePulse.EMS.Messages.BillingMessages
{
  public class GetBillingDataRequestMessage : IRequest<GetBillingDataResponseMessage>
  {
    public long BranchMediumId { get; set; }
  }
}