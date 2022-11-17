using SinePulse.EMS.Messages.BreakTimeMessages;

namespace SinePulse.EMS.UseCases.BreakTimes
{
  public interface IGetClassBreakTimeUseCase
  {
    GetClassBreakTimeResponseMessage.BreakTimeProperty GetClassBreakTime(GetClassBreakTimeRequestMessage message);
  }
}