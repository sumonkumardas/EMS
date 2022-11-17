using System;
using System.Threading.Tasks;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public interface IStudentInTimingAlertScheduler
  {
    Task ScheduleStudentInTimingAlert(DateTime scheduleDate);
  }
}