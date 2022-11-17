using FluentValidation;
using SinePulse.EMS.Messages.ClassMessages;

namespace SinePulse.EMS.UseCases.Classes
{
  public class GetSubjectsToAddInClassRequestMessageValidator : AbstractValidator<GetSubjectsToAddInClassRequestMessage>
  {
    public GetSubjectsToAddInClassRequestMessageValidator()
    {
    }
  }
}