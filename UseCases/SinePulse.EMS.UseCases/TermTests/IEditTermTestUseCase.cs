using SinePulse.EMS.Messages.TermTestMessages;

namespace SinePulse.EMS.UseCases.TermTests
{
  public interface IEditTermTestUseCase
  {
    void EditTermTest(EditTermTestRequestMessage requestMessage);
  }
}