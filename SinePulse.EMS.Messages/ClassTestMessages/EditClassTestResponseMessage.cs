using FluentValidation.Results;

namespace SinePulse.EMS.Messages.ClassTestMessages
{
  public class EditClassTestResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditClassTestResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}