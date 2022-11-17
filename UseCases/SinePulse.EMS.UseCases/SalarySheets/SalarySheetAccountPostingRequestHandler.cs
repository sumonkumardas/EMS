using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SalarySheetMessages;

namespace SinePulse.EMS.UseCases.SalarySheets
{
  public class SalarySheetAccountPostingRequestHandler: IRequestHandler<SalarySheetAccountPostingRequestMessage, SalarySheetAccountPostingResponseMessage>
  {
    private readonly SalarySheetAccountPostingRequestMessageValidator _validator;
    private readonly ISalarySheetAccountPostingUseCase _useCase;

    public SalarySheetAccountPostingRequestHandler(SalarySheetAccountPostingRequestMessageValidator validator,
      ISalarySheetAccountPostingUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<SalarySheetAccountPostingResponseMessage> Handle(SalarySheetAccountPostingRequestMessage request,
      CancellationToken cancellationToken)
    {
      SalarySheetAccountPostingResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new SalarySheetAccountPostingResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.PostSalarySheetAccount(request);

      returnMessage = new SalarySheetAccountPostingResponseMessage(validationResult, response);
      return Task.FromResult(returnMessage);
    } 
  }
}