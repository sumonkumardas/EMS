using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using Unity;

namespace SinePulse.EMS.AlertProcessingService.Externals.MassTransit.MessageHandlers
{
  public class CheckStudentAbsentAlertMessageHandler : IConsumer<CheckStudentAbsentAlertMessage>
  {
    private readonly IUnityContainer _container;

    public CheckStudentAbsentAlertMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<CheckStudentAbsentAlertMessage> context)
    {
      var messageHandler =
        _container.Resolve<AlertProcessingService.MessageHandlers.CheckStudentAbsentAlertMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}