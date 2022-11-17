using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using Unity;

namespace SinePulse.EMS.AlertProcessingService.Externals.MassTransit.MessageHandlers
{
  public class CheckStudentDelayedAlertMessageHandler : IConsumer<CheckStudentDelayedAlertMessage>
  {
    private readonly IUnityContainer _container;

    public CheckStudentDelayedAlertMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<CheckStudentDelayedAlertMessage> context)
    {
      var messageHandler =
        _container.Resolve<AlertProcessingService.MessageHandlers.CheckStudentDelayedAlertMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}