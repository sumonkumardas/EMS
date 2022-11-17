using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.OnlinePaymentMessages;

namespace SinePulse.EMS.UseCases.OnlinePayments
{
  public class GetPaymentInfoRequestHandler : IRequestHandler<GetPaymentInfoRequestMessage, GetPaymentInfoResponseMessage>
  {
    private readonly GetPaymentInfoRequestMessageValidator _validator;
    private readonly IGetPaymentInfoUseCase _useCase;

    public GetPaymentInfoRequestHandler(GetPaymentInfoRequestMessageValidator validator, IGetPaymentInfoUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetPaymentInfoResponseMessage> Handle(GetPaymentInfoRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetPaymentInfoResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetPaymentInfoResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var paymentInfo = _useCase.GetPaymentInfo(request);

      returnMessage = new GetPaymentInfoResponseMessage(validationResult, paymentInfo);
      return Task.FromResult(returnMessage);
    }
  }
}