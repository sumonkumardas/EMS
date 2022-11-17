using FluentValidation.Results;

namespace SinePulse.EMS.Messages.SessionMessages
{
  public class EditSessionResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditSessionResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}