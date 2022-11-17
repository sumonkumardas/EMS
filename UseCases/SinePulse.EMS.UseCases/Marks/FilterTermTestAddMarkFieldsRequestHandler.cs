using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.MarkMessages;

namespace SinePulse.EMS.UseCases.Marks
{
  public class FilterTermTestAddMarkFieldsRequestHandler : IRequestHandler<FilterTermTestAddMarkFieldsRequestMessage,
    FilterTermTestAddMarkFieldsResponseMessage>
  {
    private readonly FilterTermTestAddMarkFieldsRequestMessageValidator _validator;
    private readonly IFilterTermTestAddMarkFieldsUseCase _useCase;


    public FilterTermTestAddMarkFieldsRequestHandler(FilterTermTestAddMarkFieldsRequestMessageValidator validator,
      IFilterTermTestAddMarkFieldsUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
      ;
    }

    public Task<FilterTermTestAddMarkFieldsResponseMessage> Handle(FilterTermTestAddMarkFieldsRequestMessage request,
      CancellationToken cancellationToken)
    {
      FilterTermTestAddMarkFieldsResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new FilterTermTestAddMarkFieldsResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var termTests = _useCase.GetFilteredObjects(request);

      returnMessage = new FilterTermTestAddMarkFieldsResponseMessage(validationResult, termTests);
      return Task.FromResult(returnMessage);
    }
  }
}