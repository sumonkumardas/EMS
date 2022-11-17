using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.AlertProcessingMessages
{
  public class CheckStudentAbsentAlertMessage : IMessage
  {
    public long BranchMediumId { get; set; }
  }
}