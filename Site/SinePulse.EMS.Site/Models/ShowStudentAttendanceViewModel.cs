using System.Collections.Generic;
using SinePulse.EMS.Messages.AttendanceMessages;

namespace SinePulse.EMS.Site.Models
{
  public class ShowStudentAttendanceViewModel : BaseViewModel
  {
    public IEnumerable<ShowCurrentMonthAttendanceListResponseMessage.Attendance> StudentAttendances { get; set; }
  }
}