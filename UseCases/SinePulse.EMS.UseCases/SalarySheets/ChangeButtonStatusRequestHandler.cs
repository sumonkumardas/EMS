using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SalarySheetMessages;

namespace SinePulse.EMS.UseCases.SalarySheets
{
  public class ChangeButtonStatusRequestHandler : IRequestHandler<ChangeButtonStatusRequestMessage, ChangeButtonStatusResponseMessage>
  {
    private readonly ChangeButtonStatusRequestMessageValidator _validator;
    private readonly IChangeButtonStatusUseCase _useCase;

    public ChangeButtonStatusRequestHandler(ChangeButtonStatusRequestMessageValidator validator,
      IChangeButtonStatusUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ChangeButtonStatusResponseMessage> Handle(ChangeButtonStatusRequestMessage request,
      CancellationToken cancellationToken)
    {
      ChangeButtonStatusResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ChangeButtonStatusResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var buttonStatus = _useCase.ChangeButtonStatus(request);

      returnMessage = new ChangeButtonStatusResponseMessage(validationResult, buttonStatus);
      return Task.FromResult(returnMessage);
    } 
  }
}