using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using Unity;

namespace SinePulse.EMS.AlertProcessingService.Externals.MassTransit.MessageHandlers
{
  public class TermTestStartedAlertMessageHandler : IConsumer<TermTestStartedAlertMessage>
  {
    private readonly IUnityContainer _container;

    public TermTestStartedAlertMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<TermTestStartedAlertMessage> context)
    {
      var messageHandler =
        _container.Resolve<AlertProcessingService.MessageHandlers.TermTestStartedAlertMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}