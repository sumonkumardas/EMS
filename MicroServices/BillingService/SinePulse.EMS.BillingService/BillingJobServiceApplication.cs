using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using log4net;
using SinePulse.EMS.BillingService.Jobs;

namespace SinePulse.EMS.BillingService
{
  public class BillingJobServiceApplication
  {
    
    private readonly IScheduler _scheduler;
    private readonly IMidNightTaskRunner _midNightTaskRunner;
    private readonly ILog _logger;
    public BillingJobServiceApplication(IScheduler scheduler, IMidNightTaskRunner midNightTaskRunner, ILog logger)
    {
      _scheduler = scheduler;
      _midNightTaskRunner = midNightTaskRunner;
      _logger = logger;
    }

    public async Task Run()
    {
      await RunScheduler();
    }

    private async Task RunScheduler()
    {
      try
      {
        var job = CreateJob();
        job.JobDataMap.Put("midNightTaskRunner", _midNightTaskRunner);
        var trigger = CreateTrigger();
        await _scheduler.ScheduleJob(job, trigger);

      }
      catch (Exception ex)
      {
        _logger.Error(ex.Message);
      }
    }

    private static IJobDetail CreateJob()
    {
      var jobName = $"PendingBillServiceJob";
      var jobGroup = "PendingBillServiceJobGroup";
      return JobBuilder.Create<DueBillAlertMessageJob>()
        .WithIdentity(jobName, jobGroup)
        .Build();
    }

    private static ITrigger CreateTrigger()
    {
      var triggerName = $"PendingBillServiceTrigger";
      var triggerGroup = "PendingBillServiceTriggerGroup";
      var triggerBuilder = TriggerBuilder.Create().WithIdentity(triggerName, triggerGroup);
      var trigger = TriggerBuilder.Create()
        .WithIdentity(triggerName, triggerGroup)
        .StartNow()
        .WithSchedule(CronScheduleBuilder.MonthlyOnDayAndHourAndMinute(1, 0, 05))
        .Build();
      return trigger;
    }

  }
}
