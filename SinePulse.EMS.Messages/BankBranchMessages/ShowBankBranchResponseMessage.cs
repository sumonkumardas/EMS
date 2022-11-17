using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Banks;

namespace SinePulse.EMS.Messages.BankBranchMessages
{
  public class ShowBankBranchResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public BankBranchMessageModel BankBranch { get; }

    public ShowBankBranchResponseMessage(ValidationResult validationResult, BankBranchMessageModel bankBranch)
    {
      ValidationResult = validationResult;
      BankBranch = bankBranch;
    }
  }
}