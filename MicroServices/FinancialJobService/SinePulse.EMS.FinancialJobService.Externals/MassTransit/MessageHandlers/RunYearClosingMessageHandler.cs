using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.FinancialJobMessages;
using Unity;

namespace SinePulse.EMS.FinancialJobService.Externals.MassTransit.MessageHandlers
{
  public class RunYearClosingMessageHandler : IConsumer<RunYearClosingMessage>
  {
    private readonly IUnityContainer _container;

    public RunYearClosingMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<RunYearClosingMessage> context)
    {
      var messageHandler =
        _container.Resolve<FinancialJobService.MessageHandlers.RunYearClosingMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}