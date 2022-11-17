using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Attendance;

namespace SinePulse.EMS.Messages.AttendanceMessages
{
  public class ShowEmployeeAttendanceResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public EmployeeAttendanceMessageModel EmployeeAttendance { get; set; }
    public ShowEmployeeAttendanceResponseMessage(ValidationResult validationResult, EmployeeAttendanceMessageModel employeeAttendance)
    {
      ValidationResult = validationResult;
      EmployeeAttendance = employeeAttendance;
    }

    
  }
}