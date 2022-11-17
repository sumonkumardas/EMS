using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.Utility.EmailSending
{
  public class EmailSender : IEmailSender
  {
    private string FromMailAddress => Configuration.FromMailAddress;
    private string SmtpAddress => Configuration.SmtpAddress;
    private string SmtpPassword => Configuration.SmtpPassword;
    private int SmtpPort => Configuration.SmtpPort;
    public async Task<bool> SendEmail(Email mail)
    {
      if (mail.ToList.Count == 0) return false;

      return await SendEmailMessage(mail);
    }

    private async Task<bool> SendEmailMessage(Email mail)
    {
      try
      {
        var smtpAddress = FromMailAddress;
        var smtpPassword = SmtpPassword;

        var mailMessage = new MailMessage();

        foreach (var email in mail.ToList)
        {
          mailMessage.To.Add(email.Trim());
        }
        mailMessage.From = new MailAddress(FromMailAddress);
        mailMessage.Subject = mail.Subject;
        mailMessage.Body = mail.Body;
        mailMessage.IsBodyHtml = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        var smtpClient = new SmtpClient(SmtpAddress, SmtpPort)
        {
          Credentials = new NetworkCredential(smtpAddress, smtpPassword),
          EnableSsl = true,
          DeliveryMethod = SmtpDeliveryMethod.Network,
          Timeout = 10000,
        };
        smtpClient.Send(mailMessage);
        mailMessage.Dispose();
        return await Task.FromResult(true);
      }
      catch (Exception ex)
      {
        return await Task.FromResult(false);
      }
    }
  }
}
