using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeMessages;

namespace SinePulse.EMS.UseCases.Employee
{
  public class
    ShowEmployeeListRequestHandler : IRequestHandler<ShowEmployeeListRequestMessage, ShowEmployeeListResponseMessage>
  {
    private readonly ShowEmployeeListRequestMessageValidator _validator;
    private readonly IShowEmployeeListUseCase _useCase;

    public ShowEmployeeListRequestHandler(ShowEmployeeListRequestMessageValidator validator,
      IShowEmployeeListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowEmployeeListResponseMessage> Handle(ShowEmployeeListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowEmployeeListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowEmployeeListResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var employeeList = _useCase.ShowEmployeeList(request);

      returnMessage = new ShowEmployeeListResponseMessage(validationResult, employeeList);
      return Task.FromResult(returnMessage);
    }
  }
}