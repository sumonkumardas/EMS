namespace SinePulse.EMS.Messages.SeatPlanMessages
{
  public class EditSeatPlanResponseMessage
  {
    public long SeatPlanId { get; }

    public EditSeatPlanResponseMessage(long seatPlanId)
    {
      SeatPlanId = seatPlanId;
    }
  }
}