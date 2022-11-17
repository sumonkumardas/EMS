using SinePulse.EMS.Messages.TermTestMessages;

namespace SinePulse.EMS.UseCases.TermTests
{
  public interface IDisplayAddTermTestPageUseCase
  {
    DisplayAddTermTestPageResponseMessage DisplayAddTermTestPage(DisplayAddTermTestPageRequestMessage requestMessage);
  }
}