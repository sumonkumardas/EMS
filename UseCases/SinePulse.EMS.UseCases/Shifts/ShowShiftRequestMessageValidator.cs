using FluentValidation;
using SinePulse.EMS.Messages.ShiftMessages;

namespace SinePulse.EMS.UseCases.Shifts
{
  public class ShowShiftRequestMessageValidator : AbstractValidator<ShowShiftRequestMessage>
  {
    public ShowShiftRequestMessageValidator()
    {
    }
  }
}