using SinePulse.EMS.Messages.WorkingShiftMessages;

namespace SinePulse.EMS.UseCases.WorkingShifts
{
  public interface IAddWorkingShiftUseCase
  {
    void AddWorkingShift(AddWorkingShiftRequestMessage requestMessage);
  }
}