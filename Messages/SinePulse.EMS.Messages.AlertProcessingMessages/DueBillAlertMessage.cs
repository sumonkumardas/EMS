using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.AlertProcessingMessages
{
  public class DueBillAlertMessage : IMessage
  {
    public long BranchMediumId { get; set; }
  }
}