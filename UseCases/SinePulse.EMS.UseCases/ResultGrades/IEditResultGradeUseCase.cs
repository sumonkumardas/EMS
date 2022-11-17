using SinePulse.EMS.Messages.ResultGradeMessages;

namespace SinePulse.EMS.UseCases.ResultGrades
{
  public interface IEditResultGradeUseCase
  {
    EditResultGradeResponseMessage EditResultGrade(EditResultGradeRequestMessage requestMessage);
  }
}