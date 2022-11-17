using System;
using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.ScheduleJobMessages
{
  public class ScheduleClassTestStartedAlertMessage : IMessage
  {
    public long ClassTestId { get; set; }
    public DateTime ScheduleTimestamp { get; set; }
  }
}