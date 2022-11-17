using SinePulse.EMS.Domain.ServiceCharges;
using SinePulse.EMS.Messages.ServiceChargeMessages;

namespace SinePulse.EMS.UseCases.ServiceCharges
{
  public interface IAddServiceChargeUseCase
  {
    ServiceCharge AddServiceCharge(AddServiceChargeRequestMessage message);
  }
}
