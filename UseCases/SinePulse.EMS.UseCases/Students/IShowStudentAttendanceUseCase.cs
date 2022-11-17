using System.Collections.Generic;
using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Messages.Model.Attendance;

namespace SinePulse.EMS.UseCases.Students
{
  public interface IShowStudentAttendanceUseCase
  {
    StudentAttendanceMessageModel ShowStudentAttendance(ShowStudentAttendanceRequestMessage message);
  }
}