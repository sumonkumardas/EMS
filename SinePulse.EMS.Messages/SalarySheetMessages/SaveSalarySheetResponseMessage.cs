using FluentValidation.Results;

namespace SinePulse.EMS.Messages.SalarySheetMessages
{
  public class SaveSalarySheetResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public SaveSalarySheetResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}