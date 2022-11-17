using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Attendance;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.AttendanceMessages
{
  public class ShowStudentAttendanceResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public StudentAttendanceMessageModel StudentAttendance { get; set; }
    public ShowStudentAttendanceResponseMessage(ValidationResult validationResult, StudentAttendanceMessageModel studentAttendance)
    {
      ValidationResult = validationResult;
      StudentAttendance = studentAttendance;
    }


  }
}