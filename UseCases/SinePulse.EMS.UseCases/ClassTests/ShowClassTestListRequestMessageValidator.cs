using FluentValidation;
using SinePulse.EMS.Messages.ClassTestMessages;

namespace SinePulse.EMS.UseCases.ClassTests
{
  public class ShowClassTestListRequestMessageValidator : AbstractValidator<ShowClassTestListRequestMessage>
  {

    public ShowClassTestListRequestMessageValidator()
    {
    }
  }
}