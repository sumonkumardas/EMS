using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using Unity;

namespace SinePulse.EMS.AlertProcessingService.Externals.MassTransit.MessageHandlers
{
  public class ConfigureDailyAlertMessageHandler : IConsumer<ConfigureDailyAlertMessage>
  {
    private readonly IUnityContainer _container;

    public ConfigureDailyAlertMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<ConfigureDailyAlertMessage> context)
    {
      var messageHandler =
        _container.Resolve<AlertProcessingService.MessageHandlers.ConfigureDailyAlertMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}