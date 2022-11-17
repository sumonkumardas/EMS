using FluentValidation;
using SinePulse.EMS.Messages.MarkMessages;

namespace SinePulse.EMS.UseCases.Marks
{
  public class
    GetTermTestAddMarkObjectsRequestMessageValidator : AbstractValidator<GetTermTestAddMarkObjectsRequestMessage>
  {
    public GetTermTestAddMarkObjectsRequestMessageValidator()
    {
    }
  }
}