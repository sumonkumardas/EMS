using SinePulse.EMS.Messages.ClassMessages;

namespace SinePulse.EMS.UseCases.Classes
{
  public interface IAddSubjectInClassUseCase
  {
    void AddSubjectInClass(AddSubjectInClassRequestMessage requestMessage);
  }
}