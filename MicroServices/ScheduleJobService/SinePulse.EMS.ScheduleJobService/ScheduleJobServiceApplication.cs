using System;
using System.Threading.Tasks;
using System.Timers;

namespace SinePulse.EMS.ScheduleJobService
{
  public class ScheduleJobServiceApplication
  {
    private readonly Timer _timer;
    private readonly IMidNightTaskRunner _midNightTaskRunner;

    public ScheduleJobServiceApplication(Timer timer, IMidNightTaskRunner midNightTaskRunner)
    {
      _timer = timer;
      _midNightTaskRunner = midNightTaskRunner;
    }

    public Task Run()
    {
      _timer.Elapsed += async (s, o) => await RunMidNightTask();

      return Task.CompletedTask;
    }

    private async Task RunMidNightTask()
    {
      try
      {
        await _midNightTaskRunner.RunMidNightTask();
      }
      catch (Exception)
      {
      }
    }
  }
}