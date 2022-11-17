using SinePulse.EMS.Messages.StudentMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public class DisplayAddStudentPageUseCase : IDisplayAddStudentPageUseCase
  {
    public DisplayAddStudentPageResponseMessage DisplayAddStudentPage(
      DisplayAddStudentPageRequestMessage requestMessage)
    {
      return new DisplayAddStudentPageResponseMessage();
    }
  }
}