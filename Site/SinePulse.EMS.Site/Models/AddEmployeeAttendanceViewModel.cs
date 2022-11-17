using System;

namespace SinePulse.EMS.Site.Models
{
  public class AddEmployeeAttendanceViewModel : BaseViewModel
  {
    public DateTime AttendanceDate { get; set; }
    public TimeSpan InTime { get; set; }
    public TimeSpan OutTime { get; set; }
    public long EmployeeId { get; set; }
  }
}