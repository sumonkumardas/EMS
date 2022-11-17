using System;
using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.ScheduleJobMessages
{
  public class ScheduleTermTestStartedAlertMessage : IMessage
  {
    public long TermTestId { get; set; }
    public DateTime ScheduleTimestamp { get; set; }
  }
}