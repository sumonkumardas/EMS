using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ImportSessionDataMessages;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.UseCases.SeatPlans;

namespace SinePulse.EMS.UseCases.ImportSessionDatas
{
  public class ImportSessionDataRequestHandler : IRequestHandler<ImportSessionDataRequestMessage, ValidatedData<ImportSessionDataResponseMessage>>
  {
    private readonly ImportSessionDataRequestMessageValidator _validator;
    private readonly IImportSessionDataUseCase _useCase;


    public ImportSessionDataRequestHandler(ImportSessionDataRequestMessageValidator validator,
      IImportSessionDataUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ImportSessionDataResponseMessage>> Handle(ImportSessionDataRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ImportSessionDataResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ImportSessionDataResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.ImportSessionData(request);

      returnMessage = new ValidatedData<ImportSessionDataResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}