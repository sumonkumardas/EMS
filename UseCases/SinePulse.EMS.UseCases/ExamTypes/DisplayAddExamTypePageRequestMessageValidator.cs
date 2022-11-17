using FluentValidation;
using SinePulse.EMS.Messages.ExamConfigurationMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public class DisplayAddExamTypePageRequestMessageValidator : AbstractValidator<DisplayAddExamTypePageRequestMessage>
  {
    private readonly IValidTermChecker _validTermChecker;

    public DisplayAddExamTypePageRequestMessageValidator(IValidTermChecker validTermChecker)
    {
      _validTermChecker = validTermChecker;
      RuleFor(x => x.TermId).Must(IsValidTerm).WithMessage("Invalid Term");
    }

    private bool IsValidTerm(long termId)
    {
      return _validTermChecker.IsValidTerm(termId);
    }
  }
}