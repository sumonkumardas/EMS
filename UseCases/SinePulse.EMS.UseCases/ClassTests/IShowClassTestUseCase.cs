using SinePulse.EMS.Messages.ClassTestMessages;

namespace SinePulse.EMS.UseCases.ClassTests
{
  public interface IShowClassTestUseCase
  {
    ShowClassTestResponseMessage ShowClassTest(ShowClassTestRequestMessage message);
  }
}