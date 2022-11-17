using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.AcademicJobMessages
{
  public class PrepareResultSheetMessage : IMessage
  {
    public long StudentSectionId { get; set; }
    public long TermId { get; set; }
  }
}