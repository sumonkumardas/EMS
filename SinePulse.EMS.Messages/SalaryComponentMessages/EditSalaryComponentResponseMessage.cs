

using FluentValidation.Results;

namespace SinePulse.EMS.Messages.SalaryComponentMessages
{
  public class EditSalaryComponentResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditSalaryComponentResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
    
  }
}
