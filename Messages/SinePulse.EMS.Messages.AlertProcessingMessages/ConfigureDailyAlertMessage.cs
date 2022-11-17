using System;
using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.AlertProcessingMessages
{
  public class ConfigureDailyAlertMessage : IMessage
  {
    public DateTime Date { get; set; }
  }
}