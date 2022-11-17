using SinePulse.EMS.Messages.ResultGradeMessages;

namespace SinePulse.EMS.UseCases.ResultGrades
{
  public class DisplayAddResultGradePageUseCase : IDisplayAddResultGradePageUseCase
  {
    public DisplayAddResultGradePageResponseMessage DisplayAddResultGradePage(
      DisplayAddResultGradePageRequestMessage requestMessage)
    {
      return new DisplayAddResultGradePageResponseMessage();
    }
  }
}