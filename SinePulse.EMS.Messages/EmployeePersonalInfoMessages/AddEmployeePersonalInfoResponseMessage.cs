using FluentValidation.Results;

namespace SinePulse.EMS.Messages.EmployeePersonalInfoMessages
{
  public class AddEmployeePersonalInfoResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public AddEmployeePersonalInfoResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }

    
  }
}