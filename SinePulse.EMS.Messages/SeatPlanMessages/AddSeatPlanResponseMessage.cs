namespace SinePulse.EMS.Messages.SeatPlanMessages
{
  public class AddSeatPlanResponseMessage
  {
    public long SeatPlanId { get; }

    public AddSeatPlanResponseMessage(long seatPlanId)
    {
      SeatPlanId = seatPlanId;
    }
  }
}