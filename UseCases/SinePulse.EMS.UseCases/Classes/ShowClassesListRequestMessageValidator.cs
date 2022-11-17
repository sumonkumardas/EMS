using FluentValidation;
using SinePulse.EMS.Messages.ClassMessages;

namespace SinePulse.EMS.UseCases.Classes
{
  public class ShowClassesListRequestMessageValidator : AbstractValidator<ShowClassesListRequestMessage>
  {
    public ShowClassesListRequestMessageValidator()
    {
    }
  }
}