using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.FeatureMessages;

namespace SinePulse.EMS.UseCases.Features
{
  public class GetFeaturesInRoleRequestHandler : IRequestHandler<GetFeaturesInRoleRequestMessage, GetFeaturesInRoleResponseMessage>
  {
    private readonly GetFeaturesInRoleRequestMessageValidator _validator;
    private readonly IGetFeaturesInRoleUseCase _useCase;

    public GetFeaturesInRoleRequestHandler(
      GetFeaturesInRoleRequestMessageValidator validator,
      IGetFeaturesInRoleUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetFeaturesInRoleResponseMessage> Handle(
      GetFeaturesInRoleRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetFeaturesInRoleResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetFeaturesInRoleResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var features = _useCase.GetFeaturesInRole(request);

      returnMessage = new GetFeaturesInRoleResponseMessage(validationResult, features);
      return Task.FromResult(returnMessage);
    }
  }
}