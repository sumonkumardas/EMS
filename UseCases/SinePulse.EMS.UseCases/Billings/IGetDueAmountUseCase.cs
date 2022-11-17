using SinePulse.EMS.Messages.BillingMessages;

namespace SinePulse.EMS.UseCases.Billings
{
  public interface IGetDueAmountUseCase
  {
    GetDueAmountResponseMessage.PendingInfo GetDueAmount(GetDueAmountRequestMessage message);
  }
}