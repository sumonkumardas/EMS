using FluentValidation;
using SinePulse.EMS.Messages.AccountHeadMessages;

namespace SinePulse.EMS.UseCases.AccountHeads
{
  public class ShowAccountHeadRequestMessageValidator : AbstractValidator<ShowAccountHeadRequestMessage>
  {
    public ShowAccountHeadRequestMessageValidator()
    {
    }
  }
}