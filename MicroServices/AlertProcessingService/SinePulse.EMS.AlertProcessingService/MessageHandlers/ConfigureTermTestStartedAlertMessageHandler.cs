using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Services;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AlertProcessingService.MessageHandlers
{
  public class ConfigureTermTestStartedAlertMessageHandler : IMessageHandler<ConfigureTermTestStartedAlertMessage>
  {
    private readonly ITermTestStartedAlertScheduler _scheduler;

    public ConfigureTermTestStartedAlertMessageHandler(ITermTestStartedAlertScheduler scheduler)
    {
      _scheduler = scheduler;
    }

    public async Task Handle(ConfigureTermTestStartedAlertMessage message)
    {
      await _scheduler.ScheduleTermTestStartedAlert(message.TermTestId);
    }
  }
}