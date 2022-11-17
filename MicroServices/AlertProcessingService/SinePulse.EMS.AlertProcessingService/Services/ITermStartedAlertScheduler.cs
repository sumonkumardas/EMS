using System.Threading.Tasks;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public interface ITermStartedAlertScheduler
  {
    Task ScheduleTermStartedAlert(long termId);
  }
}