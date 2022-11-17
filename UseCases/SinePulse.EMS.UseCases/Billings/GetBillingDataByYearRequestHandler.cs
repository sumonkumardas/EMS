using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BillingMessages;

namespace SinePulse.EMS.UseCases.Billings
{
  public class GetBillingDataByYearRequestHandler : IRequestHandler<GetBillingDataByYearRequestMessage, GetBillingDataByYearResponseMessage>
  {
    private readonly GetBillingDataByYearRequestMessageValidator _validator;
    private readonly IGetBillingDataByYearUseCase _useCase;

    public GetBillingDataByYearRequestHandler(GetBillingDataByYearRequestMessageValidator validator,
      IGetBillingDataByYearUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetBillingDataByYearResponseMessage> Handle(GetBillingDataByYearRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetBillingDataByYearResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetBillingDataByYearResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var billingData = _useCase.GetBillingDataByYear(request);

      returnMessage = new GetBillingDataByYearResponseMessage(validationResult, billingData);
      return Task.FromResult(returnMessage);
    }
  }
}