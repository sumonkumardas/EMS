using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.ScheduleJobMessages;
using Unity;

namespace SinePulse.EMS.ScheduleJobService.Externals.MassTransit.MessageHandlers
{
  public class ScheduleCheckStudentDelayedAlertMessageHandler : IConsumer<ScheduleCheckStudentDelayedAlertMessage>
  {
    private readonly IUnityContainer _container;

    public ScheduleCheckStudentDelayedAlertMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<ScheduleCheckStudentDelayedAlertMessage> context)
    {
      var messageHandler =
        _container.Resolve<ScheduleJobService.MessageHandlers.ScheduleCheckStudentDelayedAlertMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}