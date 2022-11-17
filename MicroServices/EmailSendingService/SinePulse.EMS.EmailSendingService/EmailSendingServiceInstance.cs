using System.Threading.Tasks;

namespace SinePulse.EMS.EmailSendingService
{
  public abstract class EmailSendingServiceInstance
  {
    public abstract Task Start();

    public abstract Task Stop();
  }
}