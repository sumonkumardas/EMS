using SinePulse.EMS.Messages.Model.Routines;
using SinePulse.EMS.Messages.ClassRoutineMessages;
using System.Collections.Generic;

namespace SinePulse.EMS.UseCases.ClassRoutines
{
  public interface IShowClassRoutineListUseCase
  {
    List<ClassRoutineMessageModel> ShowClassRoutineList(ShowClassRoutineListRequestMessage request);
  }
}