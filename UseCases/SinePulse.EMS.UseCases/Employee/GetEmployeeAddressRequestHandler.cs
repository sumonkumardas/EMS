using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeAddressMessages;

namespace SinePulse.EMS.UseCases.Employee
{
  public class
    GetEmployeeAddressRequestHandler : IRequestHandler<GetEmployeeAddressRequestMessage, GetEmployeeAddressResponseMessage>
  {
    private readonly GetEmployeeAddressRequestMessageValidator _validator;
    private readonly IGetEmployeeAddressUseCase _useCase;

    public GetEmployeeAddressRequestHandler(GetEmployeeAddressRequestMessageValidator validator,
      IGetEmployeeAddressUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetEmployeeAddressResponseMessage> Handle(GetEmployeeAddressRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetEmployeeAddressResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetEmployeeAddressResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var employeeAddress = _useCase.GetEmployeeAddress(request);

      returnMessage = new GetEmployeeAddressResponseMessage(validationResult, employeeAddress);
      return Task.FromResult(returnMessage);
    }
  }
}