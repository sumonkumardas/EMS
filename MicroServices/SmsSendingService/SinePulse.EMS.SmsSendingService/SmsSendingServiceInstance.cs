using System.Threading.Tasks;

namespace SinePulse.EMS.SmsSendingService
{
  public abstract class SmsSendingServiceInstance
  {
    public abstract Task Start();

    public abstract Task Stop();
  }
}