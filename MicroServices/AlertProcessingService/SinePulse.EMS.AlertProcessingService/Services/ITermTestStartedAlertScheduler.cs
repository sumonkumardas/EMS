using System.Threading.Tasks;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public interface ITermTestStartedAlertScheduler
  {
    Task ScheduleTermTestStartedAlert(long termTestId);
  }
}