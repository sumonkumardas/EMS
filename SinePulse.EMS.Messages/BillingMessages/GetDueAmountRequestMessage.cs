using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.BillingMessages
{
  public class GetDueAmountRequestMessage : IRequest<GetDueAmountResponseMessage>
  {
    public long BranchMediumId { get; set; }
  }
}