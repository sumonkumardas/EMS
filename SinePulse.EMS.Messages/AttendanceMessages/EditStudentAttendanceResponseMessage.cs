using FluentValidation.Results;

namespace SinePulse.EMS.Messages.AttendanceMessages
{
  public class EditStudentAttendanceResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public EditStudentAttendanceResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }

    
  }
}