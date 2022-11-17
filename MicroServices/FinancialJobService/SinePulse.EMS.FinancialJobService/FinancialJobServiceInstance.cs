using System.Threading.Tasks;

namespace SinePulse.EMS.FinancialJobService
{
  public abstract class FinancialJobServiceInstance
  {
    public abstract Task Start();

    public abstract Task Stop();
  }
}