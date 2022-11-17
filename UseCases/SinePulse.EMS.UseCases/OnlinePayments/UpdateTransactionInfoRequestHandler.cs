using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.OnlinePaymentMessages;

namespace SinePulse.EMS.UseCases.OnlinePayments
{
  public class UpdateTransactionInfoRequestHandler : IRequestHandler<UpdateTransactionInfoRequestMessage,
    UpdateTransactionInfoResponseMessage>
  {
    private readonly UpdateTransactionInfoRequestMessageValidator _validator;
    private readonly IUpdateTransactionInfoUseCase _useCase;

    public UpdateTransactionInfoRequestHandler(UpdateTransactionInfoRequestMessageValidator validator,
      IUpdateTransactionInfoUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<UpdateTransactionInfoResponseMessage> Handle(UpdateTransactionInfoRequestMessage request,
      CancellationToken cancellationToken)
    {
      UpdateTransactionInfoResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new UpdateTransactionInfoResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.UpdateTransactionInfo(request);

      returnMessage = new UpdateTransactionInfoResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}