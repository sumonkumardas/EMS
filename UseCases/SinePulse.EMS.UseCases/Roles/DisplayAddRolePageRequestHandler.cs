using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.RoleMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.Roles
{
  public class DisplayAddRolePageRequestHandler
    : IRequestHandler<DisplayAddRolePageRequestMessage, ValidatedData<DisplayAddRolePageResponseMessage>>
  {
    private readonly DisplayAddRolePageRequestMessageValidator _validator;
    private readonly IDisplayAddRolePageUseCase _useCase;


    public DisplayAddRolePageRequestHandler(DisplayAddRolePageRequestMessageValidator validator,
      IDisplayAddRolePageUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<DisplayAddRolePageResponseMessage>> Handle(DisplayAddRolePageRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<DisplayAddRolePageResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<DisplayAddRolePageResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.DisplayAddRolePage(request);

      returnMessage = new ValidatedData<DisplayAddRolePageResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}