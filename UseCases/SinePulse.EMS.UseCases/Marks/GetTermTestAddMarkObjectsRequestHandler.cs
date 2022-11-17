using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.MarkMessages;

namespace SinePulse.EMS.UseCases.Marks
{
  public class GetTermTestAddMarkObjectsRequestHandler : IRequestHandler<GetTermTestAddMarkObjectsRequestMessage,
    GetTermTestAddMarkObjectsResponseMessage>
  {
    private readonly GetTermTestAddMarkObjectsRequestMessageValidator _validator;
    private readonly IGetTermTestAddMarkObjectsUseCase _useCase;


    public GetTermTestAddMarkObjectsRequestHandler(GetTermTestAddMarkObjectsRequestMessageValidator validator,
      IGetTermTestAddMarkObjectsUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetTermTestAddMarkObjectsResponseMessage> Handle(GetTermTestAddMarkObjectsRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetTermTestAddMarkObjectsResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetTermTestAddMarkObjectsResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var termTests = _useCase.GetAddMarkObjectsCollection(request);

      returnMessage = new GetTermTestAddMarkObjectsResponseMessage(validationResult, termTests);
      return Task.FromResult(returnMessage);
    }
  }
}