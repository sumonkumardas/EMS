using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.OnlinePaymentMessages;

namespace SinePulse.EMS.UseCases.OnlinePayments
{
  public class DeleteFailedTransactionRequestHandler : IRequestHandler<DeleteFailedTransactionRequestMessage, DeleteFailedTransactionResponseMessage>
  {
    private readonly DeleteFailedTransactionRequestMessageValidator _validator;
    private readonly IDeleteFailedTransactionUseCase _useCase;

    public DeleteFailedTransactionRequestHandler(DeleteFailedTransactionRequestMessageValidator validator, IDeleteFailedTransactionUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<DeleteFailedTransactionResponseMessage> Handle(DeleteFailedTransactionRequestMessage request,
      CancellationToken cancellationToken)
    {
      DeleteFailedTransactionResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new DeleteFailedTransactionResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.DeleteFailedTransaction(request);

      returnMessage = new DeleteFailedTransactionResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}