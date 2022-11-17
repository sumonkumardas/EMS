using SinePulse.EMS.Messages.MarkMessages;

namespace SinePulse.EMS.UseCases.Marks
{
  public interface IAddTermTestMarksUseCase
  {
    void AddMark(AddTermTestMarksRequestMessage requestMessage);
  }
}