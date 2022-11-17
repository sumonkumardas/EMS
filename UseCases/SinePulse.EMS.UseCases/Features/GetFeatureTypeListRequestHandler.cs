using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.FeatureMessages;

namespace SinePulse.EMS.UseCases.Features
{
  public class GetFeatureTypeListRequestHandler : IRequestHandler<GetFeatureTypeListRequestMessage,
      GetFeatureTypeListResponseMessage>
  {
    private readonly GetFeatureTypeListRequestMessageValidator _validator;
    private readonly IGetFeatureTypeListUseCase _useCase;

    public GetFeatureTypeListRequestHandler(
      GetFeatureTypeListRequestMessageValidator validator,
      IGetFeatureTypeListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetFeatureTypeListResponseMessage> Handle(
      GetFeatureTypeListRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetFeatureTypeListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetFeatureTypeListResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var featureTypeList = _useCase.GetFeatureTypeList(request);

      returnMessage = new GetFeatureTypeListResponseMessage(validationResult, featureTypeList);
      return Task.FromResult(returnMessage);
    }
  }
}