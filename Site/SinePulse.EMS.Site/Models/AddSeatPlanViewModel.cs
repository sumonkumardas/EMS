using System.Collections.Generic;

namespace SinePulse.EMS.Site.Models
{
  public class AddSeatPlanViewModel : BaseViewModel
  {
    public string RollRange { get; set; }
    public int TotalStudent { get; set; }
    public long RoomId { get; set; }
    public long TestId { get; set; }
    public long TermId { get; set; }
    public int RemainingStudent { get; set; }
    public IList<Room> Rooms { get; set; }

    public class Room
    {
      public long RoomId { get; set; }
      public string RoomText { get; set; }
    }
  }
  public class EditSeatPlanViewModel : BaseViewModel
  {
    public long SeatPlanId { get; set; }
    public string RollRange { get; set; }
    public int TotalStudent { get; set; }
    public long RoomId { get; set; }
    public long TestId { get; set; }
    public int RemainingStudent { get; set; }
    public long TermId { get; set; }
    public IList<Room> Rooms { get; set; }

    public class Room
    {
      public long RoomId { get; set; }
      public string RoomText { get; set; }
    }
  }
}