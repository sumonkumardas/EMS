using System;

namespace SinePulse.EMS.Site.Models
{
  public class AddStudentAttendanceViewModel : BaseViewModel
  {
    public long BranchMediumId { get; set; }
    public DateTime AttendanceDate { get; set; }
    public TimeSpan InTime { get; set; }
    public TimeSpan OutTime { get; set; }
    public long StudentId { get; set; }
  }
}