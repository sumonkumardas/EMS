using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BranchMediumMessages;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public class ImportCOAFromMasterRequestHandler : IRequestHandler<ImportCOAFromMasterRequestMessage, ImportCOAFromMasterResponseMessage>
  {
    private readonly ImportCOAFromMasterRequestMessageValidator _validator;
    private readonly IImportCOAFromMasterUseCase _useCase;

    public ImportCOAFromMasterRequestHandler(ImportCOAFromMasterRequestMessageValidator validator,
      IImportCOAFromMasterUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ImportCOAFromMasterResponseMessage> Handle(ImportCOAFromMasterRequestMessage request,
      CancellationToken cancellationToken)
    {
      ImportCOAFromMasterResponseMessage returnMessage;
      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ImportCOAFromMasterResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }
      _useCase.ImportCOAFromMaster(request);
      returnMessage = new ImportCOAFromMasterResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}