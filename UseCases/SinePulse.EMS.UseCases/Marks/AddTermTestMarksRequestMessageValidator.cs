using FluentValidation;
using SinePulse.EMS.Messages.MarkMessages;

namespace SinePulse.EMS.UseCases.Marks
{
  public class AddTermTestMarksRequestMessageValidator : AbstractValidator<AddTermTestMarksRequestMessage>
  {
    public AddTermTestMarksRequestMessageValidator()
    {
//      RuleFor(x => x.Comment).Must((m, x) => HasCommentIfGraceMarkIsGiven(x, m.GraceMark))
//        .WithMessage("Comment is required for Grace Mark");
      RuleFor(x => x).Must((m, x) => GreaterThanOrEqualToZero(m.ObtainedMarks)).WithMessage("Obtained Marks can not be negative.");
      RuleFor(x => x).Must((m, x) => LessThanOrEqualToFullMarks(m.ObtainedMarks, m.FullMarks))
        .WithMessage("Obtained Marks can not be greater than Full Marks.");

      RuleFor(x => x).Must((m, x) => LessThanOrEqualToFullMarks(m.GraceMarks, m.FullMarks))
        .WithMessage("Grace Marks can not be greater than Full Marks.");
      RuleFor(x => x).Must((m, x) => GreaterThanOrEqualToZero(m.GraceMarks)).WithMessage("Grace Marks can not be negative.");
      RuleFor(x => x).Must((m, x) => IsValidGraceOrObtainedMArks(m.GraceMarks, m.StudentSectionIds))
        .WithMessage("Enter Valid Grace Marks");
      RuleFor(x => x).Must((m, x) => IsValidGraceOrObtainedMArks(m.ObtainedMarks, m.StudentSectionIds))
        .WithMessage("Enter Valid Obtained Marks");
      
      RuleFor(x => x).Must((m, x) => IsValidTotalMark(m.ObtainedMarks, m.GraceMarks, m.FullMarks))
        .WithMessage("Sum of Obtained Marks and Grace Marks can not be greater than Full Marks.");
    }

    private bool IsValidTotalMark(decimal[] obtainedMarks, decimal[] graceMarks, decimal fullMarks)
    {
      if (obtainedMarks.Length == graceMarks.Length)
      {
        for (int i = 0; i < obtainedMarks.Length; i++)
        {
          if ((obtainedMarks[i] + graceMarks[i]) > fullMarks)
            return false;
        }
        return true;
      }
      return false;
    }

    private bool IsValidGraceOrObtainedMArks(decimal[] marks, long[] studentSectionIds)
    {
      return marks != null && (studentSectionIds != null && studentSectionIds.Length == marks.Length);
    }

    private bool LessThanOrEqualToFullMarks(decimal[] marks, decimal fullMarks)
    {
      if (marks != null)
        foreach (var mark in marks)
        {
          if (mark > fullMarks)
            return false;
        }

      return true;
    }

    private bool GreaterThanOrEqualToZero(decimal[] marks)
    {
      if (marks != null)
        foreach (var mark in marks)
        {
          if (mark < 0)
            return false;
        }

      return true;
    }

    private bool HasCommentIfGraceMarkIsGiven(string comment, decimal graceMark)
    {
      return graceMark == 0 || !string.IsNullOrWhiteSpace(comment);
    }
  }
}