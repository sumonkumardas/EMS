using SinePulse.EMS.Messages.BreakTimeMessages;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.UseCases.BreakTimes
{
  public interface IAddBreakTimeUseCase
  {
    BreakTimeMessageModel AddBreakTime(AddBreakTimeRequestMessage message);
  }
}