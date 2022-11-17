using SinePulse.EMS.Messages.ServiceChargeMessages;

namespace SinePulse.EMS.UseCases.ServiceCharges
{
  public interface IShowServiceChargeListUseCase
  {
    ShowServiceChargeListResponseMessage ShowServiceChargeList(ShowServiceChargeListRequestMessage message);
  }
}
