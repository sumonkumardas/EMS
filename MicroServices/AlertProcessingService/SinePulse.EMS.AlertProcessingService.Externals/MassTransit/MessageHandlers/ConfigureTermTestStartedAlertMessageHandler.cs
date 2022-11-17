using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using Unity;

namespace SinePulse.EMS.AlertProcessingService.Externals.MassTransit.MessageHandlers
{
  public class ConfigureTermTestStartedAlertMessageHandler : IConsumer<ConfigureTermTestStartedAlertMessage>
  {
    private readonly IUnityContainer _container;

    public ConfigureTermTestStartedAlertMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<ConfigureTermTestStartedAlertMessage> context)
    {
      var messageHandler =
        _container.Resolve<AlertProcessingService.MessageHandlers.ConfigureTermTestStartedAlertMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}