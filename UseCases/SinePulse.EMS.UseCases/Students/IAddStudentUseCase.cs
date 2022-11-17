using SinePulse.EMS.Messages.StudentMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public interface IAddStudentUseCase
  {
    AddStudentResponseMessage AddStudent(AddStudentRequestMessage requestMessage);
  }
}