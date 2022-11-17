using FluentValidation;
using SinePulse.EMS.Messages.AccountHeadMessages;

namespace SinePulse.EMS.UseCases.AccountHeads
{
  public class ShowAccountHeadListRequestMessageValidator : AbstractValidator<ShowAccountHeadListRequestMessage>
  {
    public ShowAccountHeadListRequestMessageValidator()
    {
    }
  }
}