using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SalarySheetMessages;

namespace SinePulse.EMS.UseCases.SalarySheets
{
  public class GetSalarySheetRequestHandler : IRequestHandler<GetSalarySheetRequestMessage, GetSalarySheetResponseMessage>
  {
    private readonly GetSalarySheetRequestMessageValidator _validator;
    private readonly IGetSalarySheetUseCase _useCase;

    public GetSalarySheetRequestHandler(GetSalarySheetRequestMessageValidator validator,
      IGetSalarySheetUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetSalarySheetResponseMessage> Handle(GetSalarySheetRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetSalarySheetResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetSalarySheetResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var employeeSalaries = _useCase.GetSalarySheet(request);

      returnMessage = new GetSalarySheetResponseMessage(validationResult, employeeSalaries);
      return Task.FromResult(returnMessage);
    } 
  }
}