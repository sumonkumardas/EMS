using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.MarkMessages
{
  public class GetTermTestAddMarkObjectsResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public AddMarkObjectsCollection AddMarkObjects { get; set; }

    public GetTermTestAddMarkObjectsResponseMessage(ValidationResult validationResult,
      AddMarkObjectsCollection addMarkObjects)
    {
      ValidationResult = validationResult;
      AddMarkObjects = addMarkObjects;
    }

    public class AddMarkObjectsCollection
    {
      public IEnumerable<Student> Students { get; set; }
      public Mark Mark { get; set; }
      public long TermTestId { get; set; }
    }

    public class Student
    {
      public long StudentSectionId { get; set; }
      public long StudentId { get; set; }
      public int Roll { get; set; }
      public string StudentName { get; set; }
    }

    public class Mark
    {
      public int PassMarks { get; set; }
      public int FullMarks { get; set; } 
    }
  }
}