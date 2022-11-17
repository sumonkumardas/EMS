using FluentValidation;
using SinePulse.EMS.Messages.ExamConfigurationMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public class EditExamConfigurationRequestMessageValidator : AbstractValidator<EditExamConfigurationRequestMessage>
  {
    private readonly IUniqueExamConfigurationPropertyChecker _uniqueExamTypePropertyChecker;

    public EditExamConfigurationRequestMessageValidator(
      IUniqueExamConfigurationPropertyChecker uniqueExamTypePropertyChecker)
    {
      _uniqueExamTypePropertyChecker = uniqueExamTypePropertyChecker;
      RuleFor(x => x.ClassId).GreaterThan(0).WithMessage("Please specify class.");
      RuleFor(x => x.SubjectId).GreaterThan(0).WithMessage("Please specify subject.");
      //RuleFor(x => x.ClassTestPercentage).GreaterThan(0).WithMessage("Please specify class test percentage.");
      RuleFor(x => x.SubjectiveFullMark).GreaterThan(0).WithMessage("Subjective Full Mark must be greater than 0.");
      RuleFor(x => x.SubjectivePassMark).GreaterThan(0).WithMessage("Subjective Pass Mark must be greater than 0.");
      RuleFor(x => x.SubjectiveFullMark)
        .Must((m, x) => IsUniqueExamConfigurationMark(m.ClassId, m.GroupId, m.SubjectId, m.TermId,m.Id))
        .WithMessage("Subjective full mark already configured for this class and group.");
      RuleFor(x => x.ObjectiveFullMark)
        .Must((m, x) => IsUniqueExamConfigurationMark(m.ClassId, m.GroupId, m.SubjectId, m.TermId, m.Id))
        .WithMessage("Objective full mark already configured for this class and group.");
      RuleFor(x => x.PracticalFullMark)
        .Must((m, x) => IsUniqueExamConfigurationMark(m.ClassId, m.GroupId, m.SubjectId, m.TermId, m.Id))
        .WithMessage("Practical full mark already configured for this class and group.");
      RuleFor(m => m.SubjectivePassMark).Must((model, field) => field <= model.SubjectiveFullMark)
        .WithMessage("pass mark must be less than full marks.");
      RuleFor(x => x.SubjectivePassMark)
        .Must((m, x) => IsUniqueExamConfigurationMark(m.ClassId, m.GroupId, m.SubjectId, m.TermId, m.Id))
        .WithMessage("Subjective pass mark already configured for this class and group.");
      RuleFor(m => m.ObjectivePassMark).Must((model, field) => field <= model.ObjectiveFullMark)
        .WithMessage("pass mark must be less than full marks.");
      RuleFor(x => x.ObjectivePassMark)
        .Must((m, x) => IsUniqueExamConfigurationMark(m.ClassId, m.GroupId, m.SubjectId, m.TermId, m.Id))
        .WithMessage("Objective pass mark already configured for this class and group.");
      RuleFor(m => m.PracticalPassMark).Must((model, field) => field <= model.PracticalFullMark)
        .WithMessage("pass mark must be less than full marks.");
      RuleFor(x => x.PracticalPassMark)
        .Must((m, x) => IsUniqueExamConfigurationMark(m.ClassId, m.GroupId, m.SubjectId, m.TermId, m.Id))
        .WithMessage("Practical pass mark already configured for this class and group.");
      RuleFor(x => x).Must((m, x) => IsMarksAlreadyAdded(m.Id))
        .WithMessage("There is already marks given for this exam configuration.");
    }

    private bool IsMarksAlreadyAdded(long examConfigurationId)
    {
      return _uniqueExamTypePropertyChecker.IsMarksAlreadyAdded(examConfigurationId);
    }

    private bool IsUniqueExamConfigurationMark(long classId, long groupId, long subjectId, long termId,long examConfigurationId)
    {
      return _uniqueExamTypePropertyChecker.IsUniqueExamConfigurationMark(classId, groupId, subjectId, termId,examConfigurationId);
    }
  }
}