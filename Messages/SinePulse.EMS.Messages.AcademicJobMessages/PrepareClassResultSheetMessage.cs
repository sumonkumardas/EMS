using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.AcademicJobMessages
{
  public class PrepareClassResultSheetMessage : IMessage
  {
    public long ClassId { get; set; }
    public long TermId { get; set; }
  }
}