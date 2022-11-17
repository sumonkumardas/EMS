using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.ExamTermMessages
{
  public class GetExamTermMarksResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public TermMarks TermMarksResponseObject { get; }

    public GetExamTermMarksResponseMessage(ValidationResult validationResult, TermMarks termMarks)
    {
      ValidationResult = validationResult;
      TermMarksResponseObject = termMarks;
    }
    
    public class TermMarks
    {
      public string TermName { get; set; }
      public IEnumerable<TermTestMark> TermTestMarks { get; set; }
    }

    public class TermTestMark
    {
      public long TermTestId { get; set; }
      public string SubjectName { get; set; }
      public int FullMarks { get; set; }
      public string ExamType { get; set; }
      public int PassMarks { get; set; }
      public MarkDetails MarkDetails { get; set; }

    }

    public class MarkDetails
    {
      public decimal ObtainedMarks { get; set; }
      public string Remarks { get; set; }
      public decimal GraceMarks { get; set; }
    }
  }
}