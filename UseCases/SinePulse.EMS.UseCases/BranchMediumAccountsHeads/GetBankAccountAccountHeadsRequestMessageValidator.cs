using FluentValidation;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;

namespace SinePulse.EMS.UseCases.BranchMediumAccountsHeads
{
  public class GetBankAccountAccountHeadsRequestMessageValidator : AbstractValidator<GetBankAccountAccountHeadsRequestMessage>
  {
    public GetBankAccountAccountHeadsRequestMessageValidator()
    {
    }
  }
}