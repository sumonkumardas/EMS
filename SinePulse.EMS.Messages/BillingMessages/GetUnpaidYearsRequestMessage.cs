using MediatR;

namespace SinePulse.EMS.Messages.BillingMessages
{
  public class GetUnpaidYearsRequestMessage : IRequest<GetUnpaidYearsResponseMessage>
  {
    public long BranchMediumId { get; set; }
  }
}