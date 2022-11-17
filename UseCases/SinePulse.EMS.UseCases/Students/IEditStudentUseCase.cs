using SinePulse.EMS.Messages.StudentMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public interface IEditStudentUseCase
  {
    void UpdateStudent(EditStudentRequestMessage message);
  }
}