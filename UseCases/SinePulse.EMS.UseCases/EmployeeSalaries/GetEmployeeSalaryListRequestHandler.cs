using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeSalaryMessages;

namespace SinePulse.EMS.UseCases.EmployeeSalaries
{
  public class GetEmployeeSalaryListRequestHandler : IRequestHandler<GetEmployeeSalaryListRequestMessage, GetEmployeeSalaryListResponseMessage>
  {
    private readonly GetEmployeeSalaryListRequestMessageValidator _validator;
    private readonly IGetEmployeeSalaryListUseCase _useCase;

    public GetEmployeeSalaryListRequestHandler(GetEmployeeSalaryListRequestMessageValidator validator, IGetEmployeeSalaryListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetEmployeeSalaryListResponseMessage> Handle(GetEmployeeSalaryListRequestMessage request, CancellationToken cancellationToken)
    {
      GetEmployeeSalaryListResponseMessage returnMessage;
      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetEmployeeSalaryListResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var employeeSalaries = _useCase.GetEmployeeSalaryList(request);

      returnMessage = new GetEmployeeSalaryListResponseMessage(validationResult, employeeSalaries);
      return Task.FromResult(returnMessage);
    }
  }
}