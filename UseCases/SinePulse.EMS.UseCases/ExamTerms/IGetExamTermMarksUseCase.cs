using SinePulse.EMS.Messages.ExamTermMessages;

namespace SinePulse.EMS.UseCases.ExamTerms
{
  public interface IGetExamTermMarksUseCase
  {
    GetExamTermMarksResponseMessage.TermMarks GetExamTermMarks(GetExamTermMarksRequestMessage requestMessage);
  }
}