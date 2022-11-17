using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.AlertProcessingMessages
{
  public class SectionResultSheetPreparedAlertMessage : IMessage
  {
    public long SectionId { get; set; }
    public long TermId { get; set; }
  }
}