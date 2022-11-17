using FluentValidation.Results;

namespace SinePulse.EMS.Messages.BankBranchMessages
{
  public class EditBankBranchResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditBankBranchResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}