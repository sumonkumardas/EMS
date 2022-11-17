using FluentValidation;
using SinePulse.EMS.Messages.ClassMessages;

namespace SinePulse.EMS.UseCases.Classes
{
  public class ShowClassRequestMessageValidator : AbstractValidator<ShowClassRequestMessage>
  {
    public ShowClassRequestMessageValidator()
    {
    }
  }
}