using FluentValidation.Results;

namespace SinePulse.EMS.Messages.TermTestMessages
{
  public class EditTermTestResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditTermTestResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}