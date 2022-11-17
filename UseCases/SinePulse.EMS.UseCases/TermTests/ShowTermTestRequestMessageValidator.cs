using FluentValidation;
using SinePulse.EMS.Messages.TermTestMessages;

namespace SinePulse.EMS.UseCases.TermTests
{
  public class ShowTermTestRequestMessageValidator : AbstractValidator<ShowTermTestRequestMessage>
  {

    public ShowTermTestRequestMessageValidator()
    {
    }
  }
}