using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.RoleMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.Roles
{
  public class ShowRoleListRequestHandler : IRequestHandler<ShowRoleListRequestMessage,
    ValidatedData<ShowRoleListResponseMessage>>
  {
    private readonly ShowRoleListRequestMessageValidator _validator;
    private readonly IShowRoleListUseCase _useCase;

    public ShowRoleListRequestHandler(ShowRoleListRequestMessageValidator validator, IShowRoleListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ShowRoleListResponseMessage>> Handle(ShowRoleListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ShowRoleListResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ShowRoleListResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.ShowRoleList(request);

      returnMessage = new ValidatedData<ShowRoleListResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}