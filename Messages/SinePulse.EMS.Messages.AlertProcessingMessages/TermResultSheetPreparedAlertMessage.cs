using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.AlertProcessingMessages
{
  public class TermResultSheetPreparedAlertMessage : IMessage
  {
    public long TermId { get; set; }
  }
}