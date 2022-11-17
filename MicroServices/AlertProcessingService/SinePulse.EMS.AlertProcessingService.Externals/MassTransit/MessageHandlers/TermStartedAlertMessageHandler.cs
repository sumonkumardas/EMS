using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using Unity;

namespace SinePulse.EMS.AlertProcessingService.Externals.MassTransit.MessageHandlers
{
  public class TermStartedAlertMessageHandler : IConsumer<TermStartedAlertMessage>
  {
    private readonly IUnityContainer _container;

    public TermStartedAlertMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<TermStartedAlertMessage> context)
    {
      var messageHandler =
        _container.Resolve<AlertProcessingService.MessageHandlers.TermStartedAlertMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}