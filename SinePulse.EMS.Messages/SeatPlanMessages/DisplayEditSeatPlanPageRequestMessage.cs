using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.SeatPlanMessages
{
  public class DisplayEditSeatPlanPageRequestMessage : IRequest<ValidatedData<DisplayEditSeatPlanPageResponseMessage>>
  {
    public long SeatPlanId { get; set; }
  }
}