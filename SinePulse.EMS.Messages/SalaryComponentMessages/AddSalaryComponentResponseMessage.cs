using FluentValidation.Results;

namespace SinePulse.EMS.Messages.SalaryComponentMessages
{
  public class AddSalaryComponentResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long SalaryComponentId { get; }

    public AddSalaryComponentResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
    public AddSalaryComponentResponseMessage(ValidationResult validationResult, long salaryComponentId)
    {
      ValidationResult = validationResult;
      SalaryComponentId = salaryComponentId;
    }
  }
}
