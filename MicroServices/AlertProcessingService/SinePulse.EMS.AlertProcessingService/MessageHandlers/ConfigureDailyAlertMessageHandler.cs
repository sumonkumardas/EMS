using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Services;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AlertProcessingService.MessageHandlers
{
  public class ConfigureDailyAlertMessageHandler : IMessageHandler<ConfigureDailyAlertMessage>
  {
    private readonly IStudentInTimingAlertScheduler _studentInTimingAlertScheduler;

    public ConfigureDailyAlertMessageHandler(IStudentInTimingAlertScheduler studentInTimingAlertScheduler)
    {
      _studentInTimingAlertScheduler = studentInTimingAlertScheduler;
    }

    public async Task Handle(ConfigureDailyAlertMessage message)
    {
      await _studentInTimingAlertScheduler.ScheduleStudentInTimingAlert(message.Date);
    }
  }
}