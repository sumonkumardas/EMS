using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.OtherComponentMessages;

namespace SinePulse.EMS.UseCases.OtherComponents
{
  public class GetOtherComponentsListRequestHandler : IRequestHandler<GetOtherComponentsListRequestMessage, GetOtherComponentsListResponseMessage>
  {
    private readonly GetOtherComponentsListRequestMessageValidator _validator;
    private readonly IGetOtherComponentsListUseCase _useCase;

    public GetOtherComponentsListRequestHandler(GetOtherComponentsListRequestMessageValidator validator, IGetOtherComponentsListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetOtherComponentsListResponseMessage> Handle(GetOtherComponentsListRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetOtherComponentsListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetOtherComponentsListResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var otherComponents = _useCase.GetOtherComponentsList(request);

      returnMessage = new GetOtherComponentsListResponseMessage(validationResult, otherComponents);
      return Task.FromResult(returnMessage);
    }
  }
}