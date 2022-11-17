using SinePulse.EMS.Domain.Attendance;
using SinePulse.EMS.Messages.AttendanceMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public interface IAddStudentAttendanceUseCase
  {
    Attendance AddStudentAttendance(AddStudentAttendanceRequestMessage request);
  }
}