using System.Threading.Tasks;
using System.Timers;
using MassTransit;
using Quartz;

namespace SinePulse.EMS.ScheduleJobService.Externals
{
  public class ScheduleJobServiceInstance : ScheduleJobService.ScheduleJobServiceInstance
  {
    private IBusControl _bus;
    private Timer _timer;
    private IScheduler _scheduler;

    public override async Task Start()
    {
      var objectGraph = new ObjectGraph();
      await objectGraph.Initialize();

      _bus = objectGraph.Bus;
      _timer = objectGraph.Timer;
      _scheduler = objectGraph.Scheduler;

      await _bus.StartAsync();

      await objectGraph.ScheduleJobServiceApplication.Run();

      _timer.Start();
      await _scheduler.Start();
    }

    public override async Task Stop()
    {
      await _scheduler.Shutdown(true);
      await _bus.StopAsync();
      _timer.Stop();
    }
  }
}