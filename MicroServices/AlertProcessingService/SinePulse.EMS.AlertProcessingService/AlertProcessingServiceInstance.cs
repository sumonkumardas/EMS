using System.Threading.Tasks;

namespace SinePulse.EMS.AlertProcessingService
{
  public abstract class AlertProcessingServiceInstance
  {
    public abstract Task Start();

    public abstract Task Stop();
  }
}