using SinePulse.EMS.Messages.ServiceChargeMessages;

namespace SinePulse.EMS.UseCases.ServiceCharges
{
  public interface IEditServiceChargeUseCase
  {
    EditServiceChargeResponseMessage EditServiceCharge(EditServiceChargeRequestMessage requestMessage);
  }
}
