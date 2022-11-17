using FluentValidation.Results;

namespace SinePulse.EMS.Messages.SalaryComponentTypeMessage
{
  public class AddSalaryComponentTypeResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long SalaryComponentTypeId { get; }

    public AddSalaryComponentTypeResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
    public AddSalaryComponentTypeResponseMessage(ValidationResult validationResult, long salaryComponentTypeId)
    {
      ValidationResult = validationResult;
      SalaryComponentTypeId = salaryComponentTypeId;
    }
  }
}
