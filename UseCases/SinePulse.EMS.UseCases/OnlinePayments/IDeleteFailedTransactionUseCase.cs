using SinePulse.EMS.Messages.OnlinePaymentMessages;

namespace SinePulse.EMS.UseCases.OnlinePayments
{
  public interface IDeleteFailedTransactionUseCase
  {
    void DeleteFailedTransaction(DeleteFailedTransactionRequestMessage message);
  }
}