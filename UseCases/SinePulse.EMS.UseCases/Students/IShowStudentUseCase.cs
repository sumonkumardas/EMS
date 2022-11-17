using SinePulse.EMS.Messages.StudentMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public interface IShowStudentUseCase
  {
    ShowStudentResponseMessage ShowStudent(ShowStudentRequestMessage message);
  }
}