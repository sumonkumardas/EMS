using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Messages.ScheduleJobMessages;
using Unity;

namespace SinePulse.EMS.ScheduleJobService.Externals.MassTransit.MessageHandlers
{
  public class ScheduleClassTestStartedAlertMessageHandler : IConsumer<ScheduleClassTestStartedAlertMessage>
  {
    private readonly IUnityContainer _container;

    public ScheduleClassTestStartedAlertMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<ScheduleClassTestStartedAlertMessage> context)
    {
      var messageHandler =
        _container.Resolve<ScheduleJobService.MessageHandlers.ScheduleClassTestStartedAlertMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}