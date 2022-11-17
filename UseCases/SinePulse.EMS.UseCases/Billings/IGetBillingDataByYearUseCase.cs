using SinePulse.EMS.Messages.BillingMessages;

namespace SinePulse.EMS.UseCases.Billings
{
  public interface IGetBillingDataByYearUseCase
  {
    GetBillingDataByYearResponseMessage.Billing GetBillingDataByYear(GetBillingDataByYearRequestMessage message);
  }
}