using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.SmsSendingMessages;
using Unity;

namespace SinePulse.EMS.SmsSendingService.Externals.MassTransit.MessageHandlers
{
  public class SmsMessageHandler : IConsumer<SmsMessage>
  {
    private readonly IUnityContainer _container;

    public SmsMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<SmsMessage> context)
    {
      var messageHandler =
        _container.Resolve<SmsSendingService.MessageHandlers.SmsMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}