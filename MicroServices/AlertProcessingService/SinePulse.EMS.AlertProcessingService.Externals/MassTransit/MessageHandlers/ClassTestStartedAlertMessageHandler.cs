using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using Unity;

namespace SinePulse.EMS.AlertProcessingService.Externals.MassTransit.MessageHandlers
{
  public class ClassTestStartedAlertMessageHandler : IConsumer<ClassTestStartedAlertMessage>
  {
    private readonly IUnityContainer _container;

    public ClassTestStartedAlertMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<ClassTestStartedAlertMessage> context)
    {
      var messageHandler =
        _container.Resolve<AlertProcessingService.MessageHandlers.ClassTestStartedAlertMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}