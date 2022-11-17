using System.Collections.Generic;

namespace SinePulse.EMS.Messages.SeatPlanMessages
{
  public class DisplayEditSeatPlanPageResponseMessage
  {
    public SeatPlan SeatPlanData { get; }
    public ICollection<Room> Rooms { get; }
    public int RemainingStudent { get; }
    public long TermId { get; set; }

    public DisplayEditSeatPlanPageResponseMessage(SeatPlan seatPlanData, ICollection<Room> rooms, int remainingStudent,long termId)
    {
      SeatPlanData = seatPlanData;
      Rooms = rooms;
      RemainingStudent = remainingStudent;
      TermId = termId;
    }

    public class SeatPlan
    {
      public long Id { get; set; }
      public string RollRange { get; set; }
      public int TotalStudent { get; set; }

      public long RoomId { get; set; }
      public long TestId { get; set; }
    }

    public class Room
    {
      public long RoomId { get; set; }
      public string RoomNo { get; set; }
      public int TotalSeat { get; set; }
      public int AvailableSeat { get; set; }
    }
  }
}