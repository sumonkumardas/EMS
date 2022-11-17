using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.AlertProcessingMessages
{
  public class ConfigureClassTestStartedAlertMessage : IMessage
  {
    public long ClassTestId { get; set; }
  }
}