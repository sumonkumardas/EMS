using FluentValidation.Results;

namespace SinePulse.EMS.Messages.TermTestMessages
{
  public class AddTermTestResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public AddTermTestResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}