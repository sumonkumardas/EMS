using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.AlertProcessingMessages
{
  public class ClassResultSheetPreparedAlertMessage : IMessage
  {
    public long ClassId { get; set; }
    public long TermId { get; set; }
  }
}