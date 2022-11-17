using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.AlertProcessingMessages
{
  public class ConfigureTermStartedAlertMessage : IMessage
  {
    public long TermId { get; set; }
  }
}