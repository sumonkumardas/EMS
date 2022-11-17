using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.AlertProcessingMessages
{
  public class ClassTestStartedAlertMessage : IMessage
  {
    public long ClassTestId { get; set; }
  }
}