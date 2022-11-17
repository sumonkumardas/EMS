using FluentValidation;
using SinePulse.EMS.Messages.ExamConfigurationMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public class AddExamConfigurationRequestMessageValidator : AbstractValidator<AddExamConfigurationRequestMessage>
  {
    private readonly IUniqueExamConfigurationPropertyChecker _uniqueExamTypePropertyChecker;

    public AddExamConfigurationRequestMessageValidator(
      IUniqueExamConfigurationPropertyChecker uniqueExamTypePropertyChecker)
    {
      _uniqueExamTypePropertyChecker = uniqueExamTypePropertyChecker;
      RuleFor(x => x.ClassId).GreaterThan(0).WithMessage("Please specify class.");
      RuleFor(x => x.SubjectId).GreaterThan(0).WithMessage("Please specify subject.");
      //RuleFor(x => x.ClassTestPercentage).GreaterThan(0).WithMessage("Please specify class test percentage.");
      RuleFor(x => x.SubjectiveFullMark).GreaterThan(0).WithMessage("Subjective Full Mark must be greater than 0.");
      RuleFor(x => x.SubjectivePassMark).GreaterThan(0).WithMessage("Subjective Pass Mark must be greater than 0.");
      RuleFor(x => x.SubjectiveFullMark)
        .Must((m, x) => IsUniqueExamConfigurationMark(m.ClassId, m.GroupId, m.SubjectId, m.TermId))
        .WithMessage("Subjective full mark already configured for this class and group.");
      RuleFor(x => x.ObjectiveFullMark)
        .Must((m, x) => IsUniqueExamConfigurationMark(m.ClassId, m.GroupId, m.SubjectId, m.TermId))
        .WithMessage("Objective full mark already configured for this class and group.");
      RuleFor(x => x.PracticalFullMark)
        .Must((m, x) => IsUniqueExamConfigurationMark(m.ClassId, m.GroupId, m.SubjectId, m.TermId))
        .WithMessage("Practical full mark already configured for this class and group.");
      RuleFor(m => m.SubjectivePassMark).Must((model, field) => field <= model.SubjectiveFullMark)
        .WithMessage("pass mark must be less than full marks.");
      RuleFor(x => x.SubjectivePassMark)
        .Must((m, x) => IsUniqueExamConfigurationMark(m.ClassId, m.GroupId, m.SubjectId, m.TermId))
        .WithMessage("Subjective pass mark already configured for this class and group.");
      RuleFor(m => m.ObjectivePassMark).Must((model, field) => field <= model.ObjectiveFullMark)
        .WithMessage("pass mark must be less than full marks.");
      RuleFor(x => x.ObjectivePassMark)
        .Must((m, x) => IsUniqueExamConfigurationMark(m.ClassId, m.GroupId, m.SubjectId, m.TermId))
        .WithMessage("Objective pass mark already configured for this class and group.");
      RuleFor(m => m.PracticalPassMark).Must((model, field) => field <= model.PracticalFullMark)
        .WithMessage("pass mark must be less than full marks.");
      RuleFor(x => x.PracticalPassMark)
        .Must((m, x) => IsUniqueExamConfigurationMark(m.ClassId, m.GroupId, m.SubjectId, m.TermId))
        .WithMessage("Practical pass mark already configured for this class and group.");
    }

    private bool IsUniqueExamConfigurationMark(long classId, long groupId, long subjectId, long termId)
    {
      return _uniqueExamTypePropertyChecker.IsUniqueExamConfigurationMark(classId, groupId, subjectId, termId);
    }
  }
}