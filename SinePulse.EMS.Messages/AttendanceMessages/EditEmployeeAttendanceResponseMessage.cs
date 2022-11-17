using FluentValidation.Results;

namespace SinePulse.EMS.Messages.AttendanceMessages
{
  public class EditEmployeeAttendanceResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public EditEmployeeAttendanceResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }

    
  }
}