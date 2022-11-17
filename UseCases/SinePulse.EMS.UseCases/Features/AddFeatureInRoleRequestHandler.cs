using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.FeatureMessages;

namespace SinePulse.EMS.UseCases.Features
{
  public class AddFeatureInRoleRequestHandler : IRequestHandler<AddFeatureInRoleRequestMessage, AddFeatureInRoleResponseMessage>
  {
    private readonly AddFeatureInRoleRequestMessageValidator _validator;
    private readonly IAddFeatureInRoleUseCase _useCase;

    public AddFeatureInRoleRequestHandler(
      AddFeatureInRoleRequestMessageValidator validator,
      IAddFeatureInRoleUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AddFeatureInRoleResponseMessage> Handle(
      AddFeatureInRoleRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddFeatureInRoleResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddFeatureInRoleResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.AddFeatureInRole(request);

      returnMessage = new AddFeatureInRoleResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}