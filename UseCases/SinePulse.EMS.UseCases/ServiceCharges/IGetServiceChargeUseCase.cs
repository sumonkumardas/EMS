using SinePulse.EMS.Messages.ServiceChargeMessages;

namespace SinePulse.EMS.UseCases.ServiceCharges
{
  public interface IGetServiceChargeUseCase
  {
    GetServiceChargeResponseMessage.ServiceCharge GetServiceCharge(GetServiceChargeRequestMessage message);
  }
}
