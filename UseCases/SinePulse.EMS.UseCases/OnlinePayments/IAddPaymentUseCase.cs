using SinePulse.EMS.Messages.OnlinePaymentMessages;

namespace SinePulse.EMS.UseCases.OnlinePayments
{
  public interface IAddPaymentUseCase
  {
    void AddPayment(AddPaymentRequestMessage message);
  }
}