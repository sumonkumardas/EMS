using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeePersonalInfoMessages;

namespace SinePulse.EMS.UseCases.EmployeePersonalInfo
{
  public class GetEmployeePersonalInfoRequestHandler : IRequestHandler<GetEmployeePersonalInfoRequestMessage,
    GetEmployeePersonalInfoResponseMessage>
  {
    private readonly GetEmployeePersonalInfoRequestMessageValidator _validator;
    private readonly IGetEmployeePersonalInfoUseCase _useCase;

    public GetEmployeePersonalInfoRequestHandler(GetEmployeePersonalInfoRequestMessageValidator validator,
      IGetEmployeePersonalInfoUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetEmployeePersonalInfoResponseMessage> Handle(GetEmployeePersonalInfoRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetEmployeePersonalInfoResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetEmployeePersonalInfoResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var employeePersonalInfo = _useCase.GetEmployeePersonalInfo(request);

      returnMessage = new GetEmployeePersonalInfoResponseMessage(validationResult, employeePersonalInfo);
      return Task.FromResult(returnMessage);
    }
  }
}