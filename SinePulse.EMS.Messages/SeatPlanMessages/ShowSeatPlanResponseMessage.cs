using SinePulse.EMS.Messages.Model.Enums;

namespace SinePulse.EMS.Messages.SeatPlanMessages
{
  public class ShowSeatPlanResponseMessage
  {
    public SeatPlan SeatPlanData { get; }

    public ShowSeatPlanResponseMessage(SeatPlan termData)
    {
      SeatPlanData = termData;
    }

    public class SeatPlan
    {
      public long Id { get; set; }
      public string RollRange { get; set; }
      public int TotalStudent { get; set; }
      public StatusEnum Status { get; set; }
      public Room Room { get; set; }
      public Test Test { set; get; }
    }

    public class Room
    {
      public long RoomId { get; set; }
      public string RoomNo { get; set; }
    }

    public class Test
    {
      public long TestId { get; set; }
      public string TestName { get; set; }
    }
  }
}