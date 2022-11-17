using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.AlertProcessingMessages
{
  public class ConfigureTermTestStartedAlertMessage : IMessage
  {
    public long TermTestId { get; set; }
  }
}