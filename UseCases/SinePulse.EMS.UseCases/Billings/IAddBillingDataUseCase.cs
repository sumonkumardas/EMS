using SinePulse.EMS.Messages.BillingMessages;

namespace SinePulse.EMS.UseCases.Billings
{
  public interface IAddBillingDataUseCase
  {
    void AddBillingData(AddBillingDataRequestMessage message);
  }
}