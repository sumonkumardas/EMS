using System.Collections.Generic;

namespace SinePulse.EMS.Messages.SeatPlanMessages
{
  public class DisplayAddSeatPlanPageResponseMessage
  {
    public ICollection<Room> Rooms { get; }
    public int RemainingStudent { get; }
    public long ExamTermId { get; set; }
    public DisplayAddSeatPlanPageResponseMessage(ICollection<Room> rooms, int remainingStudent,long examTermId)
    {
      Rooms = rooms;
      RemainingStudent = remainingStudent;
      ExamTermId = examTermId;
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