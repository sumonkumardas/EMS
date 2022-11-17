using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.FinancialJobMessages;

namespace SinePulse.EMS.FinancialJobService.Externals.MassTransit.MessageHandlers
{
  public class TestMessageHandler : IConsumer<TestMessage>
  {
    public virtual async Task Consume(ConsumeContext<TestMessage> context)
    {
      await ObjectGraph.TestMessageHandler.Handle(context.Message);
    }
  }
}