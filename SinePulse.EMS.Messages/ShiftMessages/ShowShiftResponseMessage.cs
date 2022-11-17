using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.ShiftMessages
{
  public class ShowShiftResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public ShiftMessageModel Shift { get; }

    public ShowShiftResponseMessage(ValidationResult validationResult, ShiftMessageModel shift)
    {
      ValidationResult = validationResult;
      Shift = shift;
    }
  }
}