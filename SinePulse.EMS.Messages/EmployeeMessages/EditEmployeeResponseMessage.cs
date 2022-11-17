using FluentValidation.Results;

namespace SinePulse.EMS.Messages.EmployeeMessages
{
  public class EditEmployeeResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public EditEmployeeResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }

    
  }
}