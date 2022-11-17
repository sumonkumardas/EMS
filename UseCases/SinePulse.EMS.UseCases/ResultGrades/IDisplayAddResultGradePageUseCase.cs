using SinePulse.EMS.Messages.ResultGradeMessages;

namespace SinePulse.EMS.UseCases.ResultGrades
{
  public interface IDisplayAddResultGradePageUseCase
  {
    DisplayAddResultGradePageResponseMessage DisplayAddResultGradePage(
      DisplayAddResultGradePageRequestMessage requestMessage);
  }
}