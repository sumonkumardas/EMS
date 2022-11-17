using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.MarkMessages
{
  public class GetClassTestMarksResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public ClassTest ClassTestResponseObject { get; }

    public GetClassTestMarksResponseMessage(ValidationResult validationResult, ClassTest classTestMarks)
    {
      ValidationResult = validationResult;
      ClassTestResponseObject = classTestMarks;
    }
    
    public class ClassTest
    {
      public string ClassName { get; set; }
      public int Roll { get; set; }
      public List<ClassTestMark> ClassTestMarks { get; set; }
    }

    public class ClassTestMark
    {
      public long ClassTestId { get; set; }
      public string SubjectName { get; set; }
      public int FullMarks { get; set; }
      public string ExamType { get; set; }
      public int PassMarks { get; set; }
      public decimal ObtainedMarks { get; set; }

    }
  }
}