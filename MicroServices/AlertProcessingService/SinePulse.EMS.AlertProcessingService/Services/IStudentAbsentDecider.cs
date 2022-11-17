using System;
using System.Threading.Tasks;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public interface IStudentAbsentDecider
  {
    Task<bool> IsStudentAbsent(long studentId, DateTime date, TimeSpan startTime,
      TimeSpan absentTimeBoundary);
  }
}