using SinePulse.EMS.Messages.ResultGradeMessages;

namespace SinePulse.EMS.UseCases.ResultGrades
{
  public interface IShowResultGradeUseCase
  {
    ShowResultGradeResponseMessage ShowResultGrade(ShowResultGradeRequestMessage message);
  }
}