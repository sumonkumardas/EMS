using FluentValidation.Results;

namespace SinePulse.EMS.Messages.AttendanceMessages
{
  public class AddEmployeeAttendanceResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long AttendanceId { get; set; }
    public AddEmployeeAttendanceResponseMessage(ValidationResult validationResult,long attendanceId)
    {
      ValidationResult = validationResult;
      AttendanceId = attendanceId;
    }

    
  }
}