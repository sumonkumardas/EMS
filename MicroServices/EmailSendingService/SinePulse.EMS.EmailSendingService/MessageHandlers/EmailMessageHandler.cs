using System.Threading.Tasks;
using SinePulse.EMS.Messages.EmailSendingMessages;
using SinePulse.EMS.Utility.EmailSending;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.EmailSendingService.MessageHandlers
{
  public class EmailMessageHandler : IMessageHandler<EmailMessage>
  {
    private readonly IEmailSender _emailSender;

    public EmailMessageHandler(IEmailSender emailSender)
    {
      _emailSender = emailSender;
    }

    public async Task Handle(EmailMessage message)
    {
      await _emailSender.SendEmail(new Email
      {
        ToList = message.ToList,
        BccList = message.BccList,
        Subject = message.Subject,
        Body = message.Body,
        CcList = message.CcList
      });
    }
  }
}