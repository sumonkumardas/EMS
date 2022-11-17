using System;
using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Enums;

namespace SinePulse.EMS.Messages.SeatPlanMessages
{
  public class ShowSeatPlanListResponseMessage
  {
    public List<SeatPlan> SeatPlanData { get; }

    public ShowSeatPlanListResponseMessage(List<SeatPlan> termData)
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
      public DateTime TestStartDate { get; set; }
      public DateTime TestEndDate { get; set; }
      public string ClassName { get; set; }
      public string SubjectName { get; set; }
      public string ExamType { get; set; }
    }
  }
}