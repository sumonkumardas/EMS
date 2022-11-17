using FluentValidation.Results;

namespace SinePulse.EMS.Messages.EmployeeAddressMessages
{
  public class AddEmployeeAddressResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public AddEmployeeAddressResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }

    
  }
}