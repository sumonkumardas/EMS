using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.AcademicJobMessages
{
  public class PrepareSectionResultSheetMessage : IMessage
  {
    public long SectionId { get; set; }
    public long TermId { get; set; }
  }
}