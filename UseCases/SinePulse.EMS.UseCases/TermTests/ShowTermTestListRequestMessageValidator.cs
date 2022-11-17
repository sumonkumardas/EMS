using FluentValidation;
using SinePulse.EMS.Messages.TermTestMessages;

namespace SinePulse.EMS.UseCases.TermTests
{
  public class ShowTermTestListRequestMessageValidator : AbstractValidator<ShowTermTestListRequestMessage>
  {

    public ShowTermTestListRequestMessageValidator()
    {
    }
  }
}