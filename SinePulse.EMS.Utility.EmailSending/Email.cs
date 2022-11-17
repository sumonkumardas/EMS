using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.Utility.EmailSending
{
  public class Email
  {
    public List<string> ToList { get; set; }
    public List<string> CcList { get; set; }
    public List<string> BccList { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
  }
}
