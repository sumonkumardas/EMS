using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.ClassTestMessages;

namespace SinePulse.EMS.UseCases.ClassTests
{
  public interface IEditClassTestUseCase
  {
    void EditClassTest(EditClassTestRequestMessage requestMessage);
  }
}