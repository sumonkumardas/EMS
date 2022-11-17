using FluentValidation.Results;

namespace SinePulse.EMS.Messages.ShiftMessages
{
  public class AddShiftResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long ShiftId { get; }

    public AddShiftResponseMessage(ValidationResult validationResult, long shiftId)
    {
      ValidationResult = validationResult;
      ShiftId = shiftId;
    }
    
    public AddShiftResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}