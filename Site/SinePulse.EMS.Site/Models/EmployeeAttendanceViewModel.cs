using SinePulse.EMS.Messages.Model.Enums;
using System;

namespace SinePulse.EMS.Site.Models
{
  public class EmployeeAttendanceViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public DateTime AttendanceDate { get; set; }
    public TimeSpan InTime { get; set; }
    public TimeSpan OutTime { get; set; }
    public EmployeeViewModel Employee { get; set; }
    public long EmployeeId { get; set; }
  }
}