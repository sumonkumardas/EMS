using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using Unity;

namespace SinePulse.EMS.AlertProcessingService.Externals.MassTransit.MessageHandlers
{
  public class ConfigureTermStartedAlertMessageHandler : IConsumer<ConfigureTermStartedAlertMessage>
  {
    private readonly IUnityContainer _container;

    public ConfigureTermStartedAlertMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<ConfigureTermStartedAlertMessage> context)
    {
      var messageHandler =
        _container.Resolve<AlertProcessingService.MessageHandlers.ConfigureTermStartedAlertMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}