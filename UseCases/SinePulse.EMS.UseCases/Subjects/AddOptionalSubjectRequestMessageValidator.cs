using System.Linq;
using FluentValidation;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.SubjectMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Subjects
{
  public class AddOptionalSubjectRequestMessageValidator : AbstractValidator<AddOptionalSubjectRequestMessage>
  {
    private readonly IAddOptionalSubjectPropertyChecker _addOptionalSubjectPropertyChecker;

    public AddOptionalSubjectRequestMessageValidator(
      IAddOptionalSubjectPropertyChecker addOptionalSubjectPropertyChecker)
    {
      _addOptionalSubjectPropertyChecker = addOptionalSubjectPropertyChecker;

      RuleFor(x => x.OptionalSubjectIds).Must((m, x) => IsValidSubjectIds(m.OptionalSubjectIds))
        .WithMessage("Please Select Subject.");

      RuleFor(x => x.OptionalSubjectIds).Must((m, x) =>
          IsOptionalSubjectAlreadyAdded(m.StudentId, m.OptionalSubjectIds, m.ClassId, m.Group))
        .WithMessage("Optional Subject already added.");
//      RuleFor(x => x.OptionalSubjectIds).NotNull().WithMessage("Select Subject");
      RuleFor(x => x.OptionalSubjectIds).NotEmpty().WithMessage("Select Subject");
    }

    private bool IsValidSubjectIds(long[] optionalSubjectIds)
    {
      return optionalSubjectIds != null && optionalSubjectIds.All(optionalSubjectId => optionalSubjectId > 0);
    }

    private bool IsOptionalSubjectAlreadyAdded(long studentId, long[] subjectIds, long classId, GroupEnum group)
    {
      if (subjectIds != null)
      {
        foreach (var subjectId in subjectIds)
        {
          return _addOptionalSubjectPropertyChecker.IsOptionalSubjectAlreadyAdded(studentId, subjectId, classId,
            @group);
        }
      }
      return true;
    }
  }
}