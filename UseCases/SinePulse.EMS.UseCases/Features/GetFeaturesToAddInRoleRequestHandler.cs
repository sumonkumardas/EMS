using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.FeatureMessages;

namespace SinePulse.EMS.UseCases.Features
{
  public class GetFeaturesToAddInRoleRequestHandler : IRequestHandler<GetFeaturesToAddInRoleRequestMessage,
    GetFeaturesToAddInRoleResponseMessage>
  {
    private readonly GetFeaturesToAddInRoleRequestMessageValidator _validator;
    private readonly IGetFeaturesToAddInRoleUseCase _useCase;

    public GetFeaturesToAddInRoleRequestHandler(
      GetFeaturesToAddInRoleRequestMessageValidator validator,
      IGetFeaturesToAddInRoleUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetFeaturesToAddInRoleResponseMessage> Handle(
      GetFeaturesToAddInRoleRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetFeaturesToAddInRoleResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetFeaturesToAddInRoleResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var featuresToAddInRole = _useCase.GetFeaturesToAddInRole(request);

      returnMessage = new GetFeaturesToAddInRoleResponseMessage(validationResult, featuresToAddInRole);
      return Task.FromResult(returnMessage);
    }
  }
}