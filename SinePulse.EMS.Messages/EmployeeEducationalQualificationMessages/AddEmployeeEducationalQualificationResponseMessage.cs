using FluentValidation.Results;

namespace SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages
{
  public class AddEmployeeEducationalQualificationResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public AddEmployeeEducationalQualificationResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }

    
  }
}