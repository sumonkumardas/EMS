using SinePulse.EMS.Messages.ClassMessages;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.UseCases.Classes
{
  public interface IAddClassUseCase
  {
    ClassMessageModel AddClass(AddClassRequestMessage requestMessage);
  }
}