using SinePulse.EMS.Messages.ClassRoutineMessages;

namespace SinePulse.EMS.UseCases.ClassRoutines
{
  public interface IGetAddClassRoutineViewModelDataUseCase
  {
    void GetAddClassRoutineViewModelData(GetAddClassRoutineViewModelDataRequestMessage message);
  }
}