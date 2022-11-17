using FluentValidation;
using SinePulse.EMS.Messages.BankBranchMessages;

namespace SinePulse.EMS.UseCases.BankBranches
{
  public class ShowBankBranchRequestMessageValidator : AbstractValidator<ShowBankBranchRequestMessage>
  {
    public ShowBankBranchRequestMessageValidator()
    {
    }
  }
}