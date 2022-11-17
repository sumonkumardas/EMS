using SinePulse.EMS.Messages.ExamTermMessages;
using SinePulse.EMS.Messages.MarkMessages;

namespace SinePulse.EMS.UseCases.ExamTerms
{
  public interface IGetClassTestMarkUseCase
  {
    GetClassTestMarksResponseMessage.ClassTest GetClassTestMarks(GetClassTestMarksRequestMessage requestMessage);
  }
}