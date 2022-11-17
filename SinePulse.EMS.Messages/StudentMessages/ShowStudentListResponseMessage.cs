using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Students;

namespace SinePulse.EMS.Messages.StudentMessages
{
  public class ShowStudentListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<StudentMessageModel> Students { get; }

    public ShowStudentListResponseMessage(ValidationResult validationResult, IEnumerable<StudentMessageModel> students)
    {
      ValidationResult = validationResult;
      Students = students;
    }
  }
}