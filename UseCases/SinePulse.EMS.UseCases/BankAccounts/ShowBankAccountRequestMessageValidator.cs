using FluentValidation;
using SinePulse.EMS.Messages.BankAccountMessages;

namespace SinePulse.EMS.UseCases.BankAccounts
{
  public class ShowBankAccountRequestMessageValidator : AbstractValidator<ShowBankAccountRequestMessage>
  {
    public ShowBankAccountRequestMessageValidator()
    {
    }
  }
}