using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Messages.ScheduleJobMessages;
using Unity;

namespace SinePulse.EMS.ScheduleJobService.Externals.MassTransit.MessageHandlers
{
  public class ScheduleTermTestStartedAlertMessageHandler : IConsumer<ScheduleTermTestStartedAlertMessage>
  {
    private readonly IUnityContainer _container;

    public ScheduleTermTestStartedAlertMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<ScheduleTermTestStartedAlertMessage> context)
    {
      var messageHandler =
        _container.Resolve<ScheduleJobService.MessageHandlers.ScheduleTermTestStartedAlertMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}