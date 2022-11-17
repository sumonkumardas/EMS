using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace SinePulse.EMS.Messages.AttendanceMessages
{
  public class GetAttendanceListByDateRangeResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<Attendance> AttendanceList { get; }

    public GetAttendanceListByDateRangeResponseMessage(ValidationResult validationResult,
      IEnumerable<Attendance> attendanceList)
    {
      ValidationResult = validationResult;
      AttendanceList = attendanceList;
    }

    public class Attendance
    {
      public long AttendanceId { get; set; }
      public DateTime AttendanceDate { get; set; }
      public TimeSpan? InTime { get; set; }
      public TimeSpan? OutTime { get; set; }
      public bool IsPublicHoliday { get; set; }
      public string HolidayName { get; set; }
      public long EmployeeId { get; set; }
      public long StudentId { get; set; }
      public long BranchMediumId { get; set; }
    }
  }
}