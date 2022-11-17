
namespace SinePulse.EMS.Messages.ServiceChargeMessages
{
  public class EditServiceChargeResponseMessage
  {
    public long ServiceChargeId { get; }

    public EditServiceChargeResponseMessage(long serviceChargeId)
    {
      ServiceChargeId = serviceChargeId;
    }
  }
}
