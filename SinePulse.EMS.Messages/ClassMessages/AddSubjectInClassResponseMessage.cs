using FluentValidation.Results;

namespace SinePulse.EMS.Messages.ClassMessages
{
  public class AddSubjectInClassResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public AddSubjectInClassResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}