
using System.Threading.Tasks;

namespace SinePulse.EMS.BillingService
{
  public abstract class BillingServiceInstance
  {
    public abstract Task Start();

    public abstract Task Stop();
  }
}
