using System;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Attendance
{
  public class EmployeeAttendanceMessageModel:BaseEntityMessageModel
  {
    #region Primitive Properties
    public DateTime AttendanceDate { get; set; }
    public TimeSpan? InTime { get; set; }
    public TimeSpan? OutTime { get; set; }
    #endregion

    #region  Navigation Properties
    public EmployeeMessageModel Employee { get; set; }
    #endregion
  }
}
