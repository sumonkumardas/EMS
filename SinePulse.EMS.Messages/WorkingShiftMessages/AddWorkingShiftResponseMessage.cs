using FluentValidation.Results;

namespace SinePulse.EMS.Messages.WorkingShiftMessages
{
  public class AddWorkingShiftResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public AddWorkingShiftResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}