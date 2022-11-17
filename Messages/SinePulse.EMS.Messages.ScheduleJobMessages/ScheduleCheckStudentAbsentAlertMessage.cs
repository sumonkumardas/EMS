using System;
using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.ScheduleJobMessages
{
  public class ScheduleCheckStudentAbsentAlertMessage : IMessage
  {
    public long BranchMediumId { get; set; }
    public DateTime ScheduleTimestamp { get; set; }
  }
}