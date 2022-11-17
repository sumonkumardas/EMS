using SinePulse.EMS.Messages.ResultGradeMessages;

namespace SinePulse.EMS.UseCases.ResultGrades
{
  public interface IShowResultGradeListUseCase
  {
    ShowResultGradeListResponseMessage ShowResultGradeList(ShowResultGradeListRequestMessage message);
  }
}