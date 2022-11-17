using System.Threading.Tasks;
using MassTransit;

namespace SinePulse.EMS.Utility.MessagingQueue.MassTransit
{
  public class MassTransitMessageHandler<TMessage> : IConsumer<TMessage>
    where TMessage : class
  {
    private readonly IMessageHandler<TMessage> _messageHandler;

    public MassTransitMessageHandler(IMessageHandler<TMessage> messageHandler)
    {
      _messageHandler = messageHandler;
    }

    public virtual async Task Consume(ConsumeContext<TMessage> context)
    {
      await _messageHandler.Handle(context.Message);
    }
  }
}