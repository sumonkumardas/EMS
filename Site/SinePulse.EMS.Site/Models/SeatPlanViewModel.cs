using System;
using SinePulse.EMS.Messages.Model.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class SeatPlanViewModel : BaseViewModel
  {
    public long SeatPlanId { get; set; }
    public string RollRange { get; set; }
    public int TotalStudent { get; set; }
    public Room RoomData { get; set; }
    public Test TestData { get; set; }
    public StatusEnum Status { get; set; }

    public class Room
    {
      public long RoomId { get; set; }
      public string RoomNo { get; set; }
    }

    public class Test
    {
      public long TestId { get; set; }
      public DateTime TestStartDate{ get; set; }
      public DateTime TestEndDate { get; set; }
      public string ClassName { get; set; }
      public string SubjectName { get; set; }
      public string ExamType { get; set; }
    }
  }
}