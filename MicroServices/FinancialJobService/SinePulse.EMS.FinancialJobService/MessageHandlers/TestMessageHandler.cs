using System;
using System.Threading.Tasks;
using SinePulse.EMS.Messages.FinancialJobMessages;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.FinancialJobService.MessageHandlers
{
  public class TestMessageHandler : IMessageHandler<TestMessage>
  {
    public async Task Handle(TestMessage message)
    {
      await Console.Out.WriteLineAsync($"{message.Value} is received");
    }
  }
}