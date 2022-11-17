using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.FinancialJobMessages
{
  public class RunYearClosingMessage : IMessage
  {
    public long SessionId { get; set; }
  }
}