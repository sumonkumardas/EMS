using FluentValidation.Results;

namespace SinePulse.EMS.Messages.EmployeeLeaveMessages
{
  public class AddEmployeeLeaveResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public AddEmployeeLeaveResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }

    
  }
}