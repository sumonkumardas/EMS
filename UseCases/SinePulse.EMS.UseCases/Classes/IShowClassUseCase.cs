using SinePulse.EMS.Messages.ClassMessages;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.UseCases.Classes
{
  public interface IShowClassUseCase
  {
    ClassMessageModel GetClass(ShowClassRequestMessage message);
  }
}