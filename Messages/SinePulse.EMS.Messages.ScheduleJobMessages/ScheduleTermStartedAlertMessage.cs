using System;
using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.ScheduleJobMessages
{
  public class ScheduleTermStartedAlertMessage : IMessage
  {
    public long TermId { get; set; }
    public DateTime ScheduleTimestamp { get; set; }
  }
}