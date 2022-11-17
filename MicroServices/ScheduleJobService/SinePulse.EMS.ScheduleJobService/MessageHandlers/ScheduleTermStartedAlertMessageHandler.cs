using System;
using System.Threading.Tasks;
using Quartz;
using SinePulse.EMS.Messages.ScheduleJobMessages;
using SinePulse.EMS.ScheduleJobService.Jobs;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.ScheduleJobService.MessageHandlers
{
  public class ScheduleTermStartedAlertMessageHandler : IMessageHandler<ScheduleTermStartedAlertMessage>
  {
    private readonly IScheduler _scheduler;

    public ScheduleTermStartedAlertMessageHandler(IScheduler scheduler)
    {
      _scheduler = scheduler;
    }

    public async Task Handle(ScheduleTermStartedAlertMessage message)
    {
      var job = CreateJob(message.TermId);
      var trigger = CreateTrigger(message.TermId, message.ScheduleTimestamp);
      await _scheduler.ScheduleJob(job, trigger);
    }

    private static IJobDetail CreateJob(long termId)
    {
      var jobName = $"SendTermStartedAlertMessageJob#{termId}";
      var jobGroup = "SendTermStartedAlertMessageJobGroup";
      return JobBuilder.Create<SendTermStartedAlertMessageJob>()
        .WithIdentity(jobName, jobGroup)
        .UsingJobData(Constants.TermId, termId)
        .Build();
    }

    private static ITrigger CreateTrigger(long termId, DateTime scheduleTimestamp)
    {
      var triggerName = $"SendTermStartedAlertMessageTrigger#{termId}";
      var triggerGroup = "SendTermStartedAlertMessageTriggerGroup";
      var triggerBuilder = TriggerBuilder.Create().WithIdentity(triggerName, triggerGroup);
      var trigger = triggerBuilder
        .StartAt(scheduleTimestamp)
        .WithSimpleSchedule(x => x.WithMisfireHandlingInstructionFireNow())
        .Build();
      return trigger;
    }
  }
}