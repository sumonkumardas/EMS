using System.Collections.Generic;
using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.EmailSendingMessages
{
  public class EmailMessage : IMessage
  {
    public List<string> ToList { get; set; }
    public List<string> CcList { get; set; }
    public List<string> BccList { get; set; }
    public string From { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
  }
}