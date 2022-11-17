using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Services;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AlertProcessingService.MessageHandlers
{
  public class ConfigureTermStartedAlertMessageHandler : IMessageHandler<ConfigureTermStartedAlertMessage>
  {
    private readonly ITermStartedAlertScheduler _scheduler;

    public ConfigureTermStartedAlertMessageHandler(ITermStartedAlertScheduler scheduler)
    {
      _scheduler = scheduler;
    }

    public async Task Handle(ConfigureTermStartedAlertMessage message)
    {
      await _scheduler.ScheduleTermStartedAlert(message.TermId);
    }
  }
}