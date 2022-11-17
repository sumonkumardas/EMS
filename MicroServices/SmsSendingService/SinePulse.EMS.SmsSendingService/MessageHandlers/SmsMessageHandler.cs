using System.Threading.Tasks;
using SinePulse.EMS.Messages.SmsSendingMessages;
using SinePulse.EMS.Utility.MessagingQueue;
using SinePulse.EMS.Utility.SmsSending;

namespace SinePulse.EMS.SmsSendingService.MessageHandlers
{
  public class SmsMessageHandler : IMessageHandler<SmsMessage>
  {
    private readonly ISmsSender _smsSender;

    public SmsMessageHandler(ISmsSender smsSender)
    {
      _smsSender = smsSender;
    }

    public async Task Handle(SmsMessage message)
    {
      await _smsSender.SendSms(new Sms
      {
        PhoneNumbers = message.PhoneNumbers,
        Text = message.Message
      });
    }
  }
}