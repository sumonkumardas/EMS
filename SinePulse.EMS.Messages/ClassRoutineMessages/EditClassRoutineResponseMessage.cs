using FluentValidation.Results;

namespace SinePulse.EMS.Messages.ClassRoutineMessages
{
  public class EditClassRoutineResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditClassRoutineResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}