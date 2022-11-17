using SinePulse.EMS.Messages.WaiverMessages;

namespace SinePulse.EMS.UseCases.Waivers
{
  public interface IAddWaiverUseCase
  {
    void AddWaiver(AddWaiverRequestMessage message);
  }
}