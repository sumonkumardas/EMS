using MediatR;

namespace SinePulse.EMS.Messages.BillingMessages
{
  public class GetUnpaidMonthsRequestMessage : IRequest<GetUnpaidMonthsResponseMessage>
  {
    public long BranchMediumId { get; set; }
    public int Year { get; set; }
  }
}