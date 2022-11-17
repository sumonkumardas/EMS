using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.Utility.EmailSending
{
  public interface IEmailSender
  {
    Task<bool> SendEmail(Email mail);
  }
}
