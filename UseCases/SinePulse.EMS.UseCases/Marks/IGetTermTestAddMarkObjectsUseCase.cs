using SinePulse.EMS.Messages.MarkMessages;

namespace SinePulse.EMS.UseCases.Marks
{
  public interface IGetTermTestAddMarkObjectsUseCase
  {
    GetTermTestAddMarkObjectsResponseMessage.AddMarkObjectsCollection GetAddMarkObjectsCollection(
      GetTermTestAddMarkObjectsRequestMessage message);
  }
}