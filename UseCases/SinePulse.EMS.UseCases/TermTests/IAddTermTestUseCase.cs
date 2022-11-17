using SinePulse.EMS.Messages.TermTestMessages;

namespace SinePulse.EMS.UseCases.TermTests
{
  public interface IAddTermTestUseCase
  {
    void AddTermTest(AddTermTestRequestMessage requestMessage);
  }
}