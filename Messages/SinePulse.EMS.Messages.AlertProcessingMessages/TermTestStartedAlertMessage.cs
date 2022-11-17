using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.AlertProcessingMessages
{
  public class TermTestStartedAlertMessage : IMessage
  {
    public long TermTestId { get; set; }
  }
}