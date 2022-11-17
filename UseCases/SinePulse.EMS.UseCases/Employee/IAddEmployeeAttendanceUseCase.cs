using SinePulse.EMS.Domain.Attendance;
using SinePulse.EMS.Messages.AttendanceMessages;

namespace SinePulse.EMS.UseCases.Employee
{
  public interface IAddEmployeeAttendanceUseCase
  {
    Attendance AddEmployeeAttendance(AddEmployeeAttendanceRequestMessage request);
  }
}