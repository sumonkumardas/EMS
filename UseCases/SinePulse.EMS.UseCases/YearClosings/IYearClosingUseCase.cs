using SinePulse.EMS.Messages.YearClosingMessages;

namespace SinePulse.EMS.UseCases.YearClosings
{
  public interface IYearClosingUseCase
  {
    YearClosingResponseMessage YearClosing(YearClosingRequestMessage request);
  }
}