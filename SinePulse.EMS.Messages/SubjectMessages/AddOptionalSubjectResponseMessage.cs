using FluentValidation.Results;

namespace SinePulse.EMS.Messages.SubjectMessages
{
  public class AddOptionalSubjectResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public AddOptionalSubjectResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}