using SinePulse.EMS.Messages.OnlinePaymentMessages;

namespace SinePulse.EMS.UseCases.OnlinePayments
{
  public interface IGetPaymentInfoUseCase
  {
    GetPaymentInfoResponseMessage.PaymentInfo GetPaymentInfo(GetPaymentInfoRequestMessage message);
  }
}