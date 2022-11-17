using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.ClassMessages
{
  public class GetSubjectsToAddInClassResponseMessage
  {
    public IEnumerable<SubjectMessageModel> Subjects { get; }
    public ValidationResult ValidationResult { get; }

    public GetSubjectsToAddInClassResponseMessage(ValidationResult validationResult,
      IEnumerable<SubjectMessageModel> subjects)
    {
      ValidationResult = validationResult;
      Subjects = subjects;
    }
  }
}