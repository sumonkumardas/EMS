using FluentValidation;
using SinePulse.EMS.Messages.BankMessages;

namespace SinePulse.EMS.UseCases.Banks
{
  public class ShowBankListRequestMessageValidator : AbstractValidator<ShowBankListRequestMessage>
  {
    public ShowBankListRequestMessageValidator()
    {
    }
  }
}