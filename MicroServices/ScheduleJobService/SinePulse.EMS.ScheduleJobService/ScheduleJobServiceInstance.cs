using System.Threading.Tasks;

namespace SinePulse.EMS.ScheduleJobService
{
  public abstract class ScheduleJobServiceInstance
  {
    public abstract Task Start();

    public abstract Task Stop();
  }
}