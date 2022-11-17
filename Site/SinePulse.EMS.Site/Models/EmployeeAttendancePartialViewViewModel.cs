using System;
using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Attendance;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Site.Models
{
  public class EmployeeAttendancePartialViewViewModel : BaseViewModel
  {
    public IEnumerable<EmployeeAttendanceMessageModel> EmployeeAttendances { get; set; }
    public WeekDays WeeklyHolidays { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
  }
}