using FluentValidation.Results;

namespace SinePulse.EMS.Messages.BankAccountMessages
{
  public class EditBankAccountResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditBankAccountResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}