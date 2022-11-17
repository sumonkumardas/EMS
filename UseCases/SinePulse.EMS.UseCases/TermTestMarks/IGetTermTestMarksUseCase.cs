using SinePulse.EMS.Messages.TermTestMarkMessages;

namespace SinePulse.EMS.UseCases.TermTestMarks
{
  public interface IGetTermTestMarksUseCase
  {
    GetTermTestMarksResponseMessage.TermTestMarks GetTermTestMarks(GetTermTestMarksRequestMessage message);
  }
}