using SinePulse.EMS.Messages.StudentMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public interface IDisplayAddStudentPageUseCase
  {
    DisplayAddStudentPageResponseMessage DisplayAddStudentPage(
      DisplayAddStudentPageRequestMessage requestMessage);
  }
}