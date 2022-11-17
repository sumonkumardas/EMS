using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ImportSessionDataMessages;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.ImportSessionDatas
{
  public class DisplayImportSessionDataPageRequestHandler
    : IRequestHandler<DisplayImportSessionDataPageRequestMessage, ValidatedData<DisplayImportSessionDataPageResponseMessage>>
  {
    private readonly DisplayImportSessionDataPageRequestMessageValidator _validator;
    private readonly IDisplayImportSessionDataPageUseCase _useCase;


    public DisplayImportSessionDataPageRequestHandler(DisplayImportSessionDataPageRequestMessageValidator validator,
      IDisplayImportSessionDataPageUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<DisplayImportSessionDataPageResponseMessage>> Handle(DisplayImportSessionDataPageRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<DisplayImportSessionDataPageResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<DisplayImportSessionDataPageResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.DisplayImportSessionDataPage(request);

      returnMessage = new ValidatedData<DisplayImportSessionDataPageResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}