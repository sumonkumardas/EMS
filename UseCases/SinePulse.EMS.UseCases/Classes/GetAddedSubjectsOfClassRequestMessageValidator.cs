using FluentValidation;
using SinePulse.EMS.Messages.ClassMessages;

namespace SinePulse.EMS.UseCases.Classes
{
  public class GetAddedSubjectsOfClassRequestMessageValidator : AbstractValidator<GetAddedSubjectsOfClassRequestMessage>
  {
    public GetAddedSubjectsOfClassRequestMessageValidator()
    {
    }
  }
}