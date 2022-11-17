using FluentValidation;
using SinePulse.EMS.Messages.ResultGradeMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.ResultGrades
{
  public class EditResultGradeRequestMessageValidator : AbstractValidator<EditResultGradeRequestMessage>
  {
    private readonly IUniqueResultGradePropertyChecker _uniqueResultGradePropertyChecker;
    private readonly IGradeMarksOverlappingChecker _gradeMarksOverlappingChecker;

    public EditResultGradeRequestMessageValidator(
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
      RuleFor(x => x.GradeLetter).Must((m, x) => IsUniqueResultGradeLetter(x, m.BranchMediumId,m.Id))
        .WithMessage("Grade Letter must be unique");
      RuleFor(x => x.GradePoint).Must((m, x) => IsUniqueResultGradePoint(x, m.BranchMediumId, m.Id))
        .WithMessage("Grade Point must be unique");
      RuleFor(x => x).Must(x => IsNonOverlappingGradeMarks(x.StartMark, x.EndMark, x.BranchMediumId,x.Id))
        .WithMessage("Marks have overlapped with another Result Grade");
    }

    private bool IsUniqueResultGradeLetter(string gradeLetter, long branchMediumId,long resultGradeId)
    {
      return _uniqueResultGradePropertyChecker.IsUniqueResultGradeLetter(gradeLetter, branchMediumId, resultGradeId);
    }

    private bool IsUniqueResultGradePoint(decimal gradePoint, long branchMediumId, long resultGradeId)
    {
      return _uniqueResultGradePropertyChecker.IsUniqueResultGradePoint(gradePoint, branchMediumId, resultGradeId);
    }

    private bool IsNonOverlappingGradeMarks(int startMark, int endMark, long branchMediumId, long resultGradeId)
    {
      return _gradeMarksOverlappingChecker.IsNonOverlappingGradeMarks(startMark, endMark, branchMediumId,resultGradeId);
    }
  }
}