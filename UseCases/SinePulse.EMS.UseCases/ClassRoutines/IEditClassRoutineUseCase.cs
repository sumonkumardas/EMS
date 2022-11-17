using SinePulse.EMS.Domain.Routines;
using SinePulse.EMS.Messages.ClassRoutineMessages;

namespace SinePulse.EMS.UseCases.ClassRoutines
{
  public interface IEditClassRoutineUseCase
  {
    void EditClassRoutine(EditClassRoutineRequestMessage request);
  }
}