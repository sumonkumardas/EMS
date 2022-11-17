using SinePulse.EMS.Messages.ShiftMessages;

namespace SinePulse.EMS.UseCases.Shifts
{
  public interface IEditShiftUseCase
  {
    void UpdateShift(EditShiftRequestMessage requestMessage);
  }
}