using MediatR;

namespace SinePulse.EMS.Messages.OnlinePaymentMessages
{
  public class DeleteFailedTransactionRequestMessage : IRequest<DeleteFailedTransactionResponseMessage>
  {
    public string TransactionId { get; set; }
  }
}