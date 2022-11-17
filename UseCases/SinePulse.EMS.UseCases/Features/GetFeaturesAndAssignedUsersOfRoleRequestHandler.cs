using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.FeatureMessages;

namespace SinePulse.EMS.UseCases.Features
{
  public class GetFeaturesAndAssignedUsersOfRoleRequestHandler : IRequestHandler<
    GetFeaturesAndAssignedUsersOfRoleRequestMessage, GetFeaturesAndAssignedUsersOfRoleResponseMessage>
  {
    private readonly GetFeaturesAndAssignedUsersOfRoleRequestMessageValidator _validator;
    private readonly IGetFeaturesAndAssignedUsersOfRoleUseCase _useCase;

    public GetFeaturesAndAssignedUsersOfRoleRequestHandler(
      GetFeaturesAndAssignedUsersOfRoleRequestMessageValidator validator,
      IGetFeaturesAndAssignedUsersOfRoleUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetFeaturesAndAssignedUsersOfRoleResponseMessage> Handle(
      GetFeaturesAndAssignedUsersOfRoleRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetFeaturesAndAssignedUsersOfRoleResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetFeaturesAndAssignedUsersOfRoleResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.GetFeaturesAndAssignedUsersOfRole(request);

      returnMessage = new GetFeaturesAndAssignedUsersOfRoleResponseMessage(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}