using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.SubjectMessages
{
  public class ShowSubjectListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<SubjectMessageModel> Subjects { get; }

    public ShowSubjectListResponseMessage(ValidationResult validationResult, IEnumerable<SubjectMessageModel> subjects)
    {
      ValidationResult = validationResult;
      Subjects = subjects;
    }
  }
}