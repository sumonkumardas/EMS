
using SinePulse.EMS.Messages.Model.Attendance;
using SinePulse.EMS.Messages.AttendanceMessages;

namespace SinePulse.EMS.UseCases.Employee
{
  public interface IShowEmployeeAttendanceUseCase
  {
    EmployeeAttendanceMessageModel ShowEmployeeAttendance(ShowEmployeeAttendanceRequestMessage request);
  }
}