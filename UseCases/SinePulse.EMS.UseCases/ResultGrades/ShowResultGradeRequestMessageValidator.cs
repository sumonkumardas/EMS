using FluentValidation;
using SinePulse.EMS.Messages.ResultGradeMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.ResultGrades
{
  public class ShowResultGradeRequestMessageValidator : AbstractValidator<ShowResultGradeRequestMessage>
  {
    private readonly IValidResultGradeChecker _validResultGradeChecker;

    public ShowResultGradeRequestMessageValidator(IValidResultGradeChecker validResultGradeChecker)
    {
      _validResultGradeChecker = validResultGradeChecker;

      RuleFor(x => x.ResultGradeId).Must(IsValidResultGrade).WithMessage("This Grade doesn't exists");
    }

    private bool IsValidResultGrade(long examTypeId)
    {
      return _validResultGradeChecker.IsValidResultGrade(examTypeId);
    }
  }
}