using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.SeatPlanMessages
{
  public class AddSeatPlanRequestMessage : IRequest<ValidatedData<AddSeatPlanResponseMessage>>
  {
    public string RollRange { get; set; }
    public int TotalStudent { get; set; }
    public long RoomId { get; set; }
    public long TestId { get; set; }
    public string CurrentUserName { get; set; }
  }
}