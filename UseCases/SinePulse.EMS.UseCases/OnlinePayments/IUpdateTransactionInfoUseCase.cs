using SinePulse.EMS.Messages.OnlinePaymentMessages;

namespace SinePulse.EMS.UseCases.OnlinePayments
{
  public interface IUpdateTransactionInfoUseCase
  {
    void UpdateTransactionInfo(UpdateTransactionInfoRequestMessage message);
  }
}