using FluentValidation.Results;

namespace SinePulse.EMS.Messages.AttendanceMessages
{
  public class AddStudentAttendanceResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long AttendanceId { get; set; }
    public AddStudentAttendanceResponseMessage(ValidationResult validationResult,long attendanceId)
    {
      ValidationResult = validationResult;
      AttendanceId = attendanceId;
    }

    
  }
}