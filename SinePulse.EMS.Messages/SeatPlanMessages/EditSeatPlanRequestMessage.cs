using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.SeatPlanMessages
{
  public class EditSeatPlanRequestMessage : IRequest<ValidatedData<EditSeatPlanResponseMessage>>
  {
    public long SeatPlanId { get; set; }
    public string RollRange { get; set; }
    public int TotalStudent { get; set; }
    public long RoomId { get; set; }
    public long TestId { get; set; }
    public string CurrentUserName { get; set; }
  }
}