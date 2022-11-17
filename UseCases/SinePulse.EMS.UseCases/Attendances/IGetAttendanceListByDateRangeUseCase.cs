using System.Collections.Generic;
using SinePulse.EMS.Messages.AttendanceMessages;

namespace SinePulse.EMS.UseCases.Attendances
{
  public interface IGetAttendanceListByDateRangeUseCase
  {
    IEnumerable<GetAttendanceListByDateRangeResponseMessage.Attendance>
      GetEmployeeAttendanceListByDateRange(
        GetAttendanceListByDateRangeRequestMessage requestMessage);
  }
}