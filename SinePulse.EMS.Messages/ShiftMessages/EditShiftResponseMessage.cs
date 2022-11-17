using FluentValidation.Results;

namespace SinePulse.EMS.Messages.ShiftMessages
{
  public class EditShiftResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditShiftResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}