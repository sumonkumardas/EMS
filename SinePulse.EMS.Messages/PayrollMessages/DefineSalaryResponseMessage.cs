using FluentValidation.Results;

namespace SinePulse.EMS.Messages.PayrollMessages
{
  public class DefineSalaryResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public DefineSalaryResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}