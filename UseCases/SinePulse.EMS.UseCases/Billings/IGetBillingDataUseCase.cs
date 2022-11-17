using SinePulse.EMS.Messages.BillingMessages;

namespace SinePulse.EMS.UseCases.Billings
{
  public interface IGetBillingDataUseCase
  {
    GetBillingDataResponseMessage.Billing GetBillingData(GetBillingDataRequestMessage message);
  }
}