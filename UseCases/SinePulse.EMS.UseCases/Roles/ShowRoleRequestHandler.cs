using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.RoleMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.Roles
{
  public class ShowRoleRequestHandler : IRequestHandler<ShowRoleRequestMessage, ValidatedData<ShowRoleResponseMessage>>
  {
    private readonly ShowRoleRequestMessageValidator _validator;
    private readonly IShowRoleUseCase _useCase;

    public ShowRoleRequestHandler(ShowRoleRequestMessageValidator validator, IShowRoleUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ShowRoleResponseMessage>> Handle(ShowRoleRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ShowRoleResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ShowRoleResponseMessage>(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var responseMessage = _useCase.ShowRole(request);

      returnMessage = new ValidatedData<ShowRoleResponseMessage>(validationResult, responseMessage);
      return Task.FromResult(returnMessage);
    }
  }
}