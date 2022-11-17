using System;
using System.Threading.Tasks;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public interface IStudentDelayedDecider
  {
    Task<bool> IsStudentDelayed(long studentId, DateTime date, TimeSpan startTime);
  }
}