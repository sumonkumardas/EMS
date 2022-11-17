using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.AlertProcessingMessages
{
  public class CheckStudentDelayedAlertMessage : IMessage
  {
    public long BranchMediumId { get; set; }
  }
}