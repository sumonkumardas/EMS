using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.TermTestMarkMessages
{
  public class GetTermTestMarksResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public TermTestMarks TestMarks { get; set; }

    public GetTermTestMarksResponseMessage(ValidationResult validationResult, TermTestMarks testMarks)
    {
      ValidationResult = validationResult;
      TestMarks = testMarks;
    }
    
    public class TermTestMarks
    {
      public IEnumerable<StudentMark> StudentMarks { get; set; }
      public Marks Marks { get; set; }
      public long ClassId { get; set; }
      public long SectionId { get; set; }
      public ExamTypeEnum ExamType { get; set; }
      public GroupEnum Group { get; set; }
      public long SubjectId { get; set; }
    }

    public class Marks
    {
      public int FullMarks { get; set; }
      public int PassMarks { get; set; }
      public long TermTestId { get; set; }
    }
    
    public class StudentMark
    {
      public long StudentId { get; set; }
      public string StudentName { get; set; }
      public decimal ObtainedMark { get; set; }
      public long StudentSectionId { get; set; }
      public decimal GraceMark { get; set; }
      public int Roll { get; set; }
      public string Remarks { get; set; }
    }
  }
}