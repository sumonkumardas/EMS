using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.AlertProcessingMessages
{
  public class StudentResultSheetPreparedAlertMessage : IMessage
  {
    public long StudentSectionId { get; set; }
    public long TermId { get; set; }
  }
}