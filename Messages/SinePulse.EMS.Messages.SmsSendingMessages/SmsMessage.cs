using System.Collections.Generic;
using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.SmsSendingMessages
{
  public class SmsMessage : IMessage
  {
    public List<string> PhoneNumbers { get; set; }
    public string Message { get; set; }
  }
}