using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.ShiftMessages;

namespace SinePulse.EMS.UseCases.Shifts
{
  public interface IShowShiftUseCase
  {
    ShiftMessageModel ShowShift(ShowShiftRequestMessage requestMessage);
  }
}