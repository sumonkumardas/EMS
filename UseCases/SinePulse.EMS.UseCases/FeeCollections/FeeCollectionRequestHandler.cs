using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.FeeCollection;

namespace SinePulse.EMS.UseCases.FeeCollections
{
  public class FeeCollectionRequestHandler : IRequestHandler<FeeCollectionRequestMessage, FeeCollectionResponseMessage>
  {
    private readonly FeeCollectionRequestMessageValidator _validator;
    private readonly IFeeCollectionUseCase _addAcademicFeeUseCase;

    public FeeCollectionRequestHandler(FeeCollectionRequestMessageValidator validator,
      IFeeCollectionUseCase addAcademicFeeUseCase)
    {
      _validator = validator;
      _addAcademicFeeUseCase = addAcademicFeeUseCase;
    }

    public Task<FeeCollectionResponseMessage> Handle(FeeCollectionRequestMessage request,
      CancellationToken cancellationToken)
    {
      FeeCollectionResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new FeeCollectionResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _addAcademicFeeUseCase.CollectFee(request);

      returnMessage = new FeeCollectionResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}