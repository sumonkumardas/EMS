using SinePulse.EMS.Messages.ResultGradeMessages;

namespace SinePulse.EMS.UseCases.ResultGrades
{
  public interface IAddResultGradeUseCase
  {
    AddResultGradeResponseMessage AddResultGrade(AddResultGradeRequestMessage requestMessage);
  }
}