using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;

namespace SinePulse.EMS.UseCases.EmployeeLeaves
{
  public class GetPendingLeavesRequestHandler : IRequestHandler<GetPendingLeavesRequestMessage, GetPendingLeavesResponseMessage>
  {
    private readonly GetPendingLeavesRequestMessageValidator _validator;
    private readonly IGetPendingLeavesUseCase _useCase;

    public GetPendingLeavesRequestHandler(GetPendingLeavesRequestMessageValidator validator, IGetPendingLeavesUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetPendingLeavesResponseMessage> Handle(GetPendingLeavesRequestMessage request, CancellationToken cancellationToken)
    {
      GetPendingLeavesResponseMessage returnMessage;
      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetPendingLeavesResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var pendingLeaves = _useCase.GetPendingLeaves(request);

      returnMessage = new GetPendingLeavesResponseMessage(validationResult, pendingLeaves);
      return Task.FromResult(returnMessage);
    } 
  }
}