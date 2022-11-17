using System.Collections.Generic;
using FluentValidation.Results;

namespace SinePulse.EMS.Messages.SubjectMessages
{
  public class GetOptionalSubjectListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<Subject> Subjects { get; }

    public GetOptionalSubjectListResponseMessage(ValidationResult validationResult, IEnumerable<Subject> subjects)
    {
      ValidationResult = validationResult;
      Subjects = subjects;
    }

    public class Subject
    {
      public long SubjectId { get; set; }
      public string SubjectName { get; set; }
    }
  }
}