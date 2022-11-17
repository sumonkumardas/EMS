using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.SubjectMessages
{
  public class ShowSubjectResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public SubjectMessageModel Subject { get; }

    public ShowSubjectResponseMessage(ValidationResult validationResult, SubjectMessageModel subject)
    {
      ValidationResult = validationResult;
      Subject = subject;
    }
  }
}