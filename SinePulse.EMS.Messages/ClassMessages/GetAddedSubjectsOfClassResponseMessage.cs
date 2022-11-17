using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.ClassMessages
{
  public class GetAddedSubjectsOfClassResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<SubjectWithClass> SubjectsWithClass { get; }

    public GetAddedSubjectsOfClassResponseMessage(ValidationResult validationResult,
      IEnumerable<SubjectWithClass> subjectsWithClass)
    {
      SubjectsWithClass = subjectsWithClass;
      ValidationResult = validationResult;
    }

    public class SubjectWithClass
    {
      public string SubjectName { get; set; }
      public int SubjectCode { get; set; }
      public bool IsOptional { get; set; }
    }
  }
}