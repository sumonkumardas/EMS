using System;

namespace SinePulse.EMS.Site.Models
{
  public class StudentAttendanceViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public DateTime AttendanceDate { get; set; }
    public TimeSpan InTime { get; set; }
    public TimeSpan OutTime { get; set; }
    public StudentViewModel Student { get; set; }
    public long StudentId { get; set; }
    public long BranchMediumId { get; set; }
  }
}