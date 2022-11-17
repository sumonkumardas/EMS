using SinePulse.EMS.Messages.AttendanceMessages;

namespace SinePulse.EMS.UseCases.Employee
{
  public interface IEditEmployeeAttendanceUseCase
  {
    void EditEmployeeAttendance(EditEmployeeAttendanceRequestMessage request);
  }
}