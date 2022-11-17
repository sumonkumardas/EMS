using SinePulse.EMS.Messages.MarkMessages;

namespace SinePulse.EMS.UseCases.Marks
{
  public interface IAddMarkUseCase
  {
    AddMarkResponseMessage AddMark(AddMarkRequestMessage requestMessage);
  }
}