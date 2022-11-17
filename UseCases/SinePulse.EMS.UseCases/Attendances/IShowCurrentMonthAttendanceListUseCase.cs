using System.Collections.Generic;
using SinePulse.EMS.Messages.AttendanceMessages;

namespace SinePulse.EMS.UseCases.Attendances
{
  public interface IShowCurrentMonthAttendanceListUseCase
  {
    IEnumerable<ShowCurrentMonthAttendanceListResponseMessage.Attendance> ShowEmployeeAttendanceList(
      ShowCurrentMonthAttendanceListRequestMessage request);
  }
}