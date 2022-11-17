using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.AlertProcessingMessages
{
  public class TermStartedAlertMessage : IMessage
  {
    public long TermId { get; set; }
  }
}