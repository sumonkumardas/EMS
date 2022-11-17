using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.ClassTestMessages;

namespace SinePulse.EMS.UseCases.ClassTests
{
  public interface IAddClassTestUseCase
  {
    ClassTest AddClassTest(AddClassTestRequestMessage requestMessage);
  }
}