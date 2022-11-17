using MediatR;

namespace SinePulse.EMS.Messages.BillingMessages
{
  public class GetBillingDataByYearRequestMessage : IRequest<GetBillingDataByYearResponseMessage>
  {
    public long BranchMediumId { get; set; }
    public int Year { get; set; }
  }
}