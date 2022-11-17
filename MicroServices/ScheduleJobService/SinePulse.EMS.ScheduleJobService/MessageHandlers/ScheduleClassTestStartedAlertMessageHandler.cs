using System;
using System.Threading.Tasks;
using Quartz;
using SinePulse.EMS.Messages.ScheduleJobMessages;
using SinePulse.EMS.ScheduleJobService.Jobs;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.ScheduleJobService.MessageHandlers
{
  public class ScheduleClassTestStartedAlertMessageHandler : IMessageHandler<ScheduleClassTestStartedAlertMessage>
  {
    private readonly IScheduler _scheduler;

    public ScheduleClassTestStartedAlertMessageHandler(IScheduler scheduler)
    {
      _scheduler = scheduler;
    }

    public async Task Handle(ScheduleClassTestStartedAlertMessage message)
    {
      var job = CreateJob(message.ClassTestId);
      var trigger = CreateTrigger(message.ClassTestId, message.ScheduleTimestamp);
      await _scheduler.ScheduleJob(job, trigger);
    }

    private static IJobDetail CreateJob(long classTestId)
    {
      var jobName = $"SendClassTestStartedAlertMessageJob#{classTestId}";
      var jobGroup = "SendClassTestStartedAlertMessageJobGroup";
      return JobBuilder.Create<SendClassTestStartedAlertMessageJob>()
        .WithIdentity(jobName, jobGroup)
        .UsingJobData(Constants.ClassTestId, classTestId)
        .Build();
    }

    private static ITrigger CreateTrigger(long classTestId, DateTime scheduleTimestamp)
    {
      var triggerName = $"SendClassTestStartedAlertMessageTrigger#{classTestId}";
      var triggerGroup = "SendClassTestStartedAlertMessageTriggerGroup";
      var triggerBuilder = TriggerBuilder.Create().WithIdentity(triggerName, triggerGroup);
      var trigger = triggerBuilder
        .StartAt(scheduleTimestamp)
        .WithSimpleSchedule(x => x.WithMisfireHandlingInstructionFireNow())
        .Build();
      return trigger;
    }
  }
}