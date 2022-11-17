using SinePulse.EMS.Messages.ClassTestMessage;
using SinePulse.EMS.Messages.ClassTestMessages;

namespace SinePulse.EMS.UseCases.ClassTests
{
  public interface IDisplayAddClassTestPageUseCase
  {
    DisplayAddClassTestPageResponseMessage DisplayAddClassTestPage(
      DisplayAddClassTestPageRequestMessage requestMessage);
  }
}