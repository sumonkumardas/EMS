using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.SeatPlanMessages
{
  public class DisplayAddSeatPlanPageRequestMessage : IRequest<ValidatedData<DisplayAddSeatPlanPageResponseMessage>>
  {
    public long TestId { get; set; }
  }
}