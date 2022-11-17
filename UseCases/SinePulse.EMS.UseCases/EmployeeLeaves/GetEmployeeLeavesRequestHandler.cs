using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;

namespace SinePulse.EMS.UseCases.EmployeeLeaves
{
  public class GetEmployeeLeavesRequestHandler : IRequestHandler<GetEmployeeLeavesRequestMessage, GetEmployeeLeavesResponseMessage>
  {
    private readonly GetEmployeeLeavesRequestMessageValidator _validator;
    private readonly IGetEmployeeLeavesUseCase _useCase;

    public GetEmployeeLeavesRequestHandler(GetEmployeeLeavesRequestMessageValidator validator, IGetEmployeeLeavesUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetEmployeeLeavesResponseMessage> Handle(GetEmployeeLeavesRequestMessage request, CancellationToken cancellationToken)
    {
      GetEmployeeLeavesResponseMessage returnMessage;
      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetEmployeeLeavesResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var employeeLeaves = _useCase.GetEmployeeLeaves(request);

      returnMessage = new GetEmployeeLeavesResponseMessage(validationResult, employeeLeaves);
      return Task.FromResult(returnMessage);
    }
  }
}