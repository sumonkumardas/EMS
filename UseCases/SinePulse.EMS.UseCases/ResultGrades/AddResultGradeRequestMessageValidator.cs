using FluentValidation;
using SinePulse.EMS.Messages.ResultGradeMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.ResultGrades
{
  public class AddResultGradeRequestMessageValidator : AbstractValidator<AddResultGradeRequestMessage>
  {
    private readonly IUniqueResultGradePropertyChecker _uniqueResultGradePropertyChecker;
    private readonly IGradeMarksOverlappingChecker _gradeMarksOverlappingChecker;

    public AddResultGradeRequestMessageValidator(
      IUniqueResultGradePropertyChecker uniqueResultGradePropertyChecker,
      IGradeMarksOverlappingChecker gradeMarksOverlappingChecker)
    {
      _uniqueResultGradePropertyChecker = uniqueResultGradePropertyChecker;
      _gradeMarksOverlappingChecker = gradeMarksOverlappingChecker;
      RuleFor(x => x.GradeLetter).NotEmpty().WithMessage("Please specify Grade Letter");
      RuleFor(x => x.GradeLetter).NotNull().WithMessage("Please specify Grade Letter");
      RuleFor(x => x.GradeLetter).MinimumLength(1).WithMessage("Grade Letter is too short");
      RuleFor(x => x.GradeLetter).MaximumLength(3).WithMessage("Grade Letter is too long");
      RuleFor(x => x.GradeLetter).Matches(@"[A-Z]+[\+\-]*")
        .WithMessage("Grade Letter can contains only Uppercase letter followed by optional +/-");
      RuleFor(x => x.GradeLetter).Must((m, x) => IsUniqueResultGradeLetter(x, m.BranchMediumId))
        .WithMessage("Grade Letter must be unique");
      RuleFor(x => x).Must(x => IsNonOverlappingGradeMarks(x.GradePoint,x.StartMark, x.EndMark, x.BranchMediumId))
        .WithMessage("Marks or grade point have overlapped with another Result Grade");
    }

    private bool IsUniqueResultGradeLetter(string gradeLetter, long branchMediumId)
    {
      return _uniqueResultGradePropertyChecker.IsUniqueResultGradeLetter(gradeLetter, branchMediumId);
    }

    private bool IsNonOverlappingGradeMarks(decimal gradePoint,int startMark, int endMark, long branchMediumId)
    {
      return _gradeMarksOverlappingChecker.IsNonOverlappingGradeMarks(gradePoint,startMark, endMark, branchMediumId);
    }
  }
} 