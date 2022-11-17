using FluentValidation;
using SinePulse.EMS.Messages.ShiftMessages;

namespace SinePulse.EMS.UseCases.Shifts
{
  public class ShowShiftListRequestMessageValidator : AbstractValidator<ShowShiftListRequestMessage>
  {
    public ShowShiftListRequestMessageValidator()
    {
    }
  }
}