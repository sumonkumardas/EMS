using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BillingMessages;

namespace SinePulse.EMS.UseCases.Billings
{
  public class GetBillingDataRequestHandler : IRequestHandler<GetBillingDataRequestMessage, GetBillingDataResponseMessage>
  {
    private readonly GetBillingDataRequestMessageValidator _validator;
    private readonly IGetBillingDataUseCase _useCase;

    public GetBillingDataRequestHandler(GetBillingDataRequestMessageValidator validator,
      IGetBillingDataUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetBillingDataResponseMessage> Handle(GetBillingDataRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetBillingDataResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetBillingDataResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var billingData = _useCase.GetBillingData(request);

      returnMessage = new GetBillingDataResponseMessage(validationResult, billingData);
      return Task.FromResult(returnMessage);
    }
  }
}