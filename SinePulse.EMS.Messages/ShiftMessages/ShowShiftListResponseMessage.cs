using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.ShiftMessages
{
  public class ShowShiftListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<ShiftMessageModel> Shifts { get; }

    public ShowShiftListResponseMessage(ValidationResult validationResult, IEnumerable<ShiftMessageModel> shifts)
    {
      ValidationResult = validationResult;
      Shifts = shifts;
    }
  }
}