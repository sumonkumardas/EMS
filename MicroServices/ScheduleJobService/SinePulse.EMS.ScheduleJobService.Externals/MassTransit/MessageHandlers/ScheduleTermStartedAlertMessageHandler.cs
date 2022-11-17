using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.ScheduleJobMessages;
using Unity;

namespace SinePulse.EMS.ScheduleJobService.Externals.MassTransit.MessageHandlers
{
  public class ScheduleTermStartedAlertMessageHandler : IConsumer<ScheduleTermStartedAlertMessage>
  {
    private readonly IUnityContainer _container;

    public ScheduleTermStartedAlertMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<ScheduleTermStartedAlertMessage> context)
    {
      var messageHandler =
        _container.Resolve<ScheduleJobService.MessageHandlers.ScheduleTermStartedAlertMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}