using FluentValidation;
using SinePulse.EMS.Messages.SubjectMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Subjects
{
  public class AddSubjectRequestMessageValidator : AbstractValidator<AddSubjectRequestMessage>
  {
    private readonly IUniqueSubjectPropertyChecker _uniqueSubjectPropertyChecker;

    public AddSubjectRequestMessageValidator(IUniqueSubjectPropertyChecker uniqueSubjectPropertyChecker)
    {
      _uniqueSubjectPropertyChecker = uniqueSubjectPropertyChecker;

      RuleFor(x => x.SubjectName).NotEmpty().WithMessage("Please specify Subject name");
      RuleFor(x => x.SubjectName).NotNull().WithMessage("Please specify Subject name");
      RuleFor(x => x.SubjectName).MaximumLength(250).WithMessage("Subject name is too long");
      RuleFor(x => x.SubjectName).Must(IsUniqueSubjectName).WithMessage("Subject name already exists.");
      RuleFor(x => x.SubjectCode).NotEmpty().WithMessage("Please specify Subject Code");
      RuleFor(x => x.SubjectCode).NotNull().WithMessage("Please specify Subject Code");
      //RuleFor(x => x.SubjectCode).MaximumLength(250).WithMessage("Subject Code is too long");
      RuleFor(x => x.SubjectCode).Must(IsUniqueSubjectCode).WithMessage("Subject Code already exists.");
    }
    
    private bool IsUniqueSubjectCode(int subjectCode)
    {
      return _uniqueSubjectPropertyChecker.IsUniqueSubjectCode(subjectCode);
    }

    private bool IsUniqueSubjectName(string subjectName)
    {
      return _uniqueSubjectPropertyChecker.IsUniqueSubjectName(subjectName);
    }
  }
}