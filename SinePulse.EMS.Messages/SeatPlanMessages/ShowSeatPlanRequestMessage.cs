using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.SeatPlanMessages
{
  public class ShowSeatPlanRequestMessage : IRequest<ValidatedData<ShowSeatPlanResponseMessage>>
  {
    public long SeatPlanId { get; set; }
  }
}