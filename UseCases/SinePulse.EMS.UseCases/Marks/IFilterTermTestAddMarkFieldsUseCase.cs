using SinePulse.EMS.Messages.MarkMessages;

namespace SinePulse.EMS.UseCases.Marks
{
  public interface IFilterTermTestAddMarkFieldsUseCase
  {
    FilterTermTestAddMarkFieldsResponseMessage.TermTestAddMarkObjectsCollection GetFilteredObjects(FilterTermTestAddMarkFieldsRequestMessage message);
  }
}