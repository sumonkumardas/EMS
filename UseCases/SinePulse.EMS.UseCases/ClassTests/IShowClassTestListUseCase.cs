using SinePulse.EMS.Messages.ClassTestMessages;

namespace SinePulse.EMS.UseCases.ClassTests
{
  public interface IShowClassTestListUseCase
  {
    ShowClassTestListResponseMessage ShowClassTestList(ShowClassTestListRequestMessage message);
  }
}