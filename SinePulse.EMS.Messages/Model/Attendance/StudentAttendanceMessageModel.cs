using SinePulse.EMS.Messages.Model.Shared;
using SinePulse.EMS.Messages.Model.Students;
using System;

namespace SinePulse.EMS.Messages.Model.Attendance
{
  public class StudentAttendanceMessageModel:BaseEntityMessageModel
  {
    #region Primitive Properties
    public DateTime AttendanceDate { get; set; }
    public TimeSpan InTime { get; set; }
    public TimeSpan OutTime { get; set; }
    #endregion
    #region  Navigation Properties
    public StudentMessageModel Student { get; set; }
    #endregion
  }
}
