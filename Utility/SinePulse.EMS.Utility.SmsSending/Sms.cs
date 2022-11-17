using System.Collections.Generic;

namespace SinePulse.EMS.Utility.SmsSending
{
  public class Sms
  {
    public IList<string> PhoneNumbers { get; set; }
    public string Text { get; set; }
  }
}