
using SinePulse.EMS.Messages.AttendanceMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public interface IEditStudentAttendanceUseCase
  {
    void EditStudentAttendance(EditStudentAttendanceRequestMessage request);
  }
}