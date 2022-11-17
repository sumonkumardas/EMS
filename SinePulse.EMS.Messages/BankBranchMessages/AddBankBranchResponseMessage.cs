using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Banks;

namespace SinePulse.EMS.Messages.BankBranchMessages
{
  public class AddBankBranchResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public BankBranchMessageModel BankBranch { get; }

    public AddBankBranchResponseMessage(ValidationResult validationResult, BankBranchMessageModel bankBranch)
    {
      ValidationResult = validationResult;
      BankBranch = bankBranch;
    }
  }
}