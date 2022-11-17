using FluentValidation;
using SinePulse.EMS.Messages.ClassMessages;

namespace SinePulse.EMS.UseCases.Classes
{
  public class AddSubjectInClassRequestMessageValidator : AbstractValidator<AddSubjectInClassRequestMessage>
  {
    public AddSubjectInClassRequestMessageValidator()
    {
      RuleFor(x => x.SubjectIds).NotNull().WithMessage("Select Subject");
      RuleFor(x => x.SubjectIds).NotEmpty().WithMessage("Select Subject");
    }
  }
}