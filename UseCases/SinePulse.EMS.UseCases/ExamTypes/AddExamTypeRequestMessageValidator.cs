using FluentValidation;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.ExamConfigurationMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public class AddExamTypeRequestMessageValidator : AbstractValidator<AddExamTypeRequestMessage>
  {
    private readonly IUniqueExamTypePropertyChecker _uniqueExamTypePropertyChecker;

    public AddExamTypeRequestMessageValidator(IUniqueExamTypePropertyChecker uniqueExamTypePropertyChecker)
    {
      _uniqueExamTypePropertyChecker = uniqueExamTypePropertyChecker;
      RuleFor(x => x.ExamTypeName).NotEmpty().WithMessage("Please specify ExamType Name");
      RuleFor(x => x.ExamTypeName).NotNull().WithMessage("Please specify ExamType Name");
      //RuleFor(x => x.ExamTypeName).Must((m, x) => IsUniqueExamTypeName(x, m.TermId, m.GroupSubjectsId));
    }

    private bool IsUniqueExamTypeName(ExamTypeEnum termName, long termId, long groupSubjectsId)
    {
      return _uniqueExamTypePropertyChecker.IsUniqueExamTypeName(termName, termId, groupSubjectsId);
    }
  }
}