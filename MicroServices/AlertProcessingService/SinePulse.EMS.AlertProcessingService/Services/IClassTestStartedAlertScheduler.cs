using System.Threading.Tasks;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public interface IClassTestStartedAlertScheduler
  {
    Task ScheduleClassTestStartedAlert(long classTestId);
  }
}