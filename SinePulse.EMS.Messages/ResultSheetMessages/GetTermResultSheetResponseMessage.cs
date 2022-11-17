using System.Collections.Generic;
using FluentValidation.Results;

namespace SinePulse.EMS.Messages.ResultSheetMessages
{
  public class GetTermResultSheetResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public ExamTermResult TermResult { get; }

    public GetTermResultSheetResponseMessage(ValidationResult validationResult, ExamTermResult termResult)
    {
      ValidationResult = validationResult;
      TermResult = termResult;
    }

    public class ExamTermResult
    {
      public decimal Gpa { get; set; }
      public string GradeLetter { get; set; }
      public decimal TotalMark { get; set; }
      public IEnumerable<SubjectWiseResult> SubjectWiseResults { get; set; }
    }

    public class SubjectWiseResult
    {
      public string SubjectName { get; set; }
      public string GradeLetter { get; set; }
      public decimal GradePoint { get; set; }
      public decimal SubjectiveMark { get; set; }
      public decimal ObjectiveMark { get; set; }
      public decimal PracticalMark { get; set; }
      public decimal ClassTestMark { get; set; }
      public decimal TotalMark { get; set; }
    }
  }
}