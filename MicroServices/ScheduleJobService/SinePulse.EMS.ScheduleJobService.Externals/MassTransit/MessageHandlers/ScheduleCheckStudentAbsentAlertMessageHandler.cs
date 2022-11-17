using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.ScheduleJobMessages;
using Unity;

namespace SinePulse.EMS.ScheduleJobService.Externals.MassTransit.MessageHandlers
{
  public class ScheduleCheckStudentAbsentAlertMessageHandler : IConsumer<ScheduleCheckStudentAbsentAlertMessage>
  {
    private readonly IUnityContainer _container;

    public ScheduleCheckStudentAbsentAlertMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<ScheduleCheckStudentAbsentAlertMessage> context)
    {
      var messageHandler =
        _container.Resolve<ScheduleJobService.MessageHandlers.ScheduleCheckStudentAbsentAlertMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}