using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using Unity;

namespace SinePulse.EMS.AlertProcessingService.Externals.MassTransit.MessageHandlers
{
  public class ConfigureClassTestStartedAlertMessageHandler : IConsumer<ConfigureClassTestStartedAlertMessage>
  {
    private readonly IUnityContainer _container;

    public ConfigureClassTestStartedAlertMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<ConfigureClassTestStartedAlertMessage> context)
    {
      var messageHandler =
        _container.Resolve<AlertProcessingService.MessageHandlers.ConfigureClassTestStartedAlertMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}