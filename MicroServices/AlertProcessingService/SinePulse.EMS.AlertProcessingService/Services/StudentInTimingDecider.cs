using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Attendance;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public class StudentInTimingDecider : IStudentDelayedDecider, IStudentAbsentDecider
  {
    private readonly IRepository _repository;

    public StudentInTimingDecider(IRepository repository)
    {
      _repository = repository;
    }

    public async Task<bool> IsStudentDelayed(long studentId, DateTime date, TimeSpan startTime)
    {
      var attendance = await _repository
        .Filter<Attendance>(x => x.Student.Id == studentId && x.AttendanceDate == date)
        .FirstOrDefaultAsync();

      return attendance?.InTime == null || startTime < CountUptoMinutes(attendance.InTime.Value);
    }

    public async Task<bool> IsStudentAbsent(long studentId, DateTime date, TimeSpan startTime,
      TimeSpan absentTimeBoundary)
    {
      var attendance = await _repository
        .Filter<Attendance>(x => x.Student.Id == studentId && x.AttendanceDate == date)
        .FirstOrDefaultAsync();

      return attendance?.InTime == null || startTime + absentTimeBoundary < CountUptoMinutes(attendance.InTime.Value);
    }

    private static TimeSpan CountUptoMinutes(TimeSpan recordedTime)
    {
      return new TimeSpan(recordedTime.Days, recordedTime.Hours, recordedTime.Minutes, 0);
    }
  }
}