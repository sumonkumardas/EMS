using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.FeatureMessages;

namespace SinePulse.EMS.UseCases.Features
{
  public class RemoveFeatureFromRoleRequestHandler : IRequestHandler<RemoveFeatureFromRoleRequestMessage,
    RemoveFeatureFromRoleResponseMessage>
  {
    private readonly RemoveFeatureFromRoleRequestMessageValidator _validator;
    private readonly IRemoveFeatureFromRoleUseCase _useCase;

    public RemoveFeatureFromRoleRequestHandler(RemoveFeatureFromRoleRequestMessageValidator validator,
      IRemoveFeatureFromRoleUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<RemoveFeatureFromRoleResponseMessage> Handle(RemoveFeatureFromRoleRequestMessage request,
      CancellationToken cancellationToken)
    {
      RemoveFeatureFromRoleResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new RemoveFeatureFromRoleResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.RemoveFeatureFromRole(request);

      returnMessage = new RemoveFeatureFromRoleResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}