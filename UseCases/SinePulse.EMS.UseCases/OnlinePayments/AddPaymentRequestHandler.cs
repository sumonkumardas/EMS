using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.OnlinePaymentMessages;

namespace SinePulse.EMS.UseCases.OnlinePayments
{
  public class AddPaymentRequestHandler : IRequestHandler<AddPaymentRequestMessage, AddPaymentResponseMessage>
  {
    private readonly AddPaymentRequestMessageValidator _validator;
    private readonly IAddPaymentUseCase _useCase;

    public AddPaymentRequestHandler(AddPaymentRequestMessageValidator validator, IAddPaymentUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AddPaymentResponseMessage> Handle(AddPaymentRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddPaymentResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddPaymentResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.AddPayment(request);

      returnMessage = new AddPaymentResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}