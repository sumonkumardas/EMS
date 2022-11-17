using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Services;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AlertProcessingService.MessageHandlers
{
  public class ConfigureClassTestStartedAlertMessageHandler : IMessageHandler<ConfigureClassTestStartedAlertMessage>
  {
    private readonly IClassTestStartedAlertScheduler _scheduler;

    public ConfigureClassTestStartedAlertMessageHandler(IClassTestStartedAlertScheduler scheduler)
    {
      _scheduler = scheduler;
    }

    public async Task Handle(ConfigureClassTestStartedAlertMessage message)
    {
      await _scheduler.ScheduleClassTestStartedAlert(message.ClassTestId);
    }
  }
}