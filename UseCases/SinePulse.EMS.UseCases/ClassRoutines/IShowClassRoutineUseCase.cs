using SinePulse.EMS.Messages.Model.Routines;
using SinePulse.EMS.Messages.ClassRoutineMessages;

namespace SinePulse.EMS.UseCases.ClassRoutines
{
  public interface IShowClassRoutineUseCase
  {
    ClassRoutineMessageModel ShowClassRoutine(ShowClassRoutineRequestMessage request);
  }
}