using FluentValidation;
using SinePulse.EMS.Messages.MarkMessages;

namespace SinePulse.EMS.UseCases.Marks
{
  public class FilterTermTestAddMarkFieldsRequestMessageValidator : AbstractValidator<FilterTermTestAddMarkFieldsRequestMessage>
  {
    public FilterTermTestAddMarkFieldsRequestMessageValidator()
    {
    }
  }
}