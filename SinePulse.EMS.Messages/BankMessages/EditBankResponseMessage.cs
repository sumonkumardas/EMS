using FluentValidation.Results;

namespace SinePulse.EMS.Messages.BankMessages
{
  public class EditBankResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long BankId { get; }

    public EditBankResponseMessage(ValidationResult validationResult, long bankId)
    {
      ValidationResult = validationResult;
      BankId = bankId;
    }
  }
}