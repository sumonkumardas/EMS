using FluentValidation.Results;

namespace SinePulse.EMS.Messages.ClassRoutineMessages
{
  public class AddClassRoutineResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long ClassRoutineId { get; }

    public AddClassRoutineResponseMessage(ValidationResult validationResult, long classRoutineId)
    {
      ValidationResult = validationResult;
      ClassRoutineId = classRoutineId;
    }
  }
}