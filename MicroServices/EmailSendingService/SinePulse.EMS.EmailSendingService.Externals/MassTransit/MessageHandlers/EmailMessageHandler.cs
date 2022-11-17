using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.EmailSendingMessages;
using Unity;

namespace SinePulse.EMS.EmailSendingService.Externals.MassTransit.MessageHandlers
{
  public class EmailMessageHandler : IConsumer<EmailMessage>
  {
    private readonly IUnityContainer _container;

    public EmailMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<EmailMessage> context)
    {
      var messageHandler =
        _container.Resolve<EmailSendingService.MessageHandlers.EmailMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}