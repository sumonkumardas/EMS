using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.ShiftMessages;

namespace SinePulse.EMS.UseCases.Shifts
{
  public interface IShowShiftListUseCase
  {
    IEnumerable<ShiftMessageModel> ShowShiftList(ShowShiftListRequestMessage requestMessage);
  }
}