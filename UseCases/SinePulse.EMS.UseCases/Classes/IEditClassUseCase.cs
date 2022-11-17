using SinePulse.EMS.Messages.ClassMessages;

namespace SinePulse.EMS.UseCases.Classes
{
  public interface IEditClassUseCase
  {
    void EditClass(EditClassRequestMessage message);
  }
}