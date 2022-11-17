using FluentValidation;
using SinePulse.EMS.Messages.BankMessages;

namespace SinePulse.EMS.UseCases.Banks
{
  public class ShowBankRequestMessageValidator : AbstractValidator<ShowBankRequestMessage>
  {
    public ShowBankRequestMessageValidator()
    {
    }
  }
}