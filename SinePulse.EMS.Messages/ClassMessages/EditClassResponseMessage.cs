using FluentValidation.Results;

namespace SinePulse.EMS.Messages.ClassMessages
{
  public class EditClassResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditClassResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}