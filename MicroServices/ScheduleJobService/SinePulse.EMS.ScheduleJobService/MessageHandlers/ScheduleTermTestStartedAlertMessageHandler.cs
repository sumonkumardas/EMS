using System;
using System.Threading.Tasks;
using Quartz;
using SinePulse.EMS.Messages.ScheduleJobMessages;
using SinePulse.EMS.ScheduleJobService.Jobs;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.ScheduleJobService.MessageHandlers
{
  public class ScheduleTermTestStartedAlertMessageHandler : IMessageHandler<ScheduleTermTestStartedAlertMessage>
  {
    private readonly IScheduler _scheduler;

    public ScheduleTermTestStartedAlertMessageHandler(IScheduler scheduler)
    {
      _scheduler = scheduler;
    }

    public async Task Handle(ScheduleTermTestStartedAlertMessage message)
    {
      var job = CreateJob(message.TermTestId);
      var trigger = CreateTrigger(message.TermTestId, message.ScheduleTimestamp);
      await _scheduler.ScheduleJob(job, trigger);
    }

    private static IJobDetail CreateJob(long termTestId)
    {
      var jobName = $"SendTermTestStartedAlertMessageJob#{termTestId}";
      var jobGroup = "SendTermTestStartedAlertMessageJobGroup";
      return JobBuilder.Create<SendTermTestStartedAlertMessageJob>()
        .WithIdentity(jobName, jobGroup)
        .UsingJobData(Constants.TermTestId, termTestId)
        .Build();
    }

    private static ITrigger CreateTrigger(long termTestId, DateTime scheduleTimestamp)
    {
      var triggerName = $"SendTermTestStartedAlertMessageTrigger#{termTestId}";
      var triggerGroup = "SendTermTestStartedAlertMessageTriggerGroup";
      var triggerBuilder = TriggerBuilder.Create().WithIdentity(triggerName, triggerGroup);
      var trigger = triggerBuilder
        .StartAt(scheduleTimestamp)
        .WithSimpleSchedule(x => x.WithMisfireHandlingInstructionFireNow())
        .Build();
      return trigger;
    }
  }
}