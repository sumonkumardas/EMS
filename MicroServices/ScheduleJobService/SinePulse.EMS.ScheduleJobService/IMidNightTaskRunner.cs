using System.Threading.Tasks;

namespace SinePulse.EMS.ScheduleJobService
{
  public interface IMidNightTaskRunner
  {
    Task RunMidNightTask();
  }
}