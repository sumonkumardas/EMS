using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using SinePulse.EMS.BillingService.Jobs;
using SinePulse.EMS.Messages.BillingMessages;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.BillingService
{
  public class ScheduleBillingAlertMessageHandler : IMessageHandler<DueBillAlertMessage>
  {
    private readonly IScheduler _scheduler;

    public ScheduleBillingAlertMessageHandler(IScheduler scheduler)
    {
      _scheduler = scheduler;
    }

    public async Task Handle(DueBillAlertMessage message)
    {
      var job = CreateJob(message.BranchMediumId);
      var trigger = CreateTrigger(message.BranchMediumId, message.ScheduleTimestamp);
      await _scheduler.ScheduleJob(job, trigger);
    }

    private static IJobDetail CreateJob(long branchMediumId)
    {
      var jobName = $"DueBillAlertMessageJob#{branchMediumId}";
      var jobGroup = "DueBillAlertMessageJobGroup";
      return JobBuilder.Create<DueBillAlertMessageJob>()
        .WithIdentity(jobName, jobGroup)
        .UsingJobData("BranchMediumId", branchMediumId)
        .Build();
    }

    private static ITrigger CreateTrigger(long branchMediumId, DateTime scheduleTimestamp)
    {
      var triggerName = $"DueBillAlertMessageTrigger#{branchMediumId}";
      var triggerGroup = "DueBillAlertMessageTriggerGroup";
      var triggerBuilder = TriggerBuilder.Create().WithIdentity(triggerName, triggerGroup);
      var trigger = triggerBuilder
        .StartAt(scheduleTimestamp)
        .WithSimpleSchedule(x => x.WithMisfireHandlingInstructionFireNow())
        .Build();
      return trigger;
    }
  }
}
