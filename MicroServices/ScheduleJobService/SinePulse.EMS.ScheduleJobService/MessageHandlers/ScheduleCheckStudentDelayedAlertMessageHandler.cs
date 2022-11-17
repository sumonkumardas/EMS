using System;
using System.Threading.Tasks;
using Quartz;
using SinePulse.EMS.Messages.ScheduleJobMessages;
using SinePulse.EMS.ScheduleJobService.Jobs;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.ScheduleJobService.MessageHandlers
{
  public class ScheduleCheckStudentDelayedAlertMessageHandler : IMessageHandler<ScheduleCheckStudentDelayedAlertMessage>
  {
    private readonly IScheduler _scheduler;

    public ScheduleCheckStudentDelayedAlertMessageHandler(IScheduler scheduler)
    {
      _scheduler = scheduler;
    }

    public async Task Handle(ScheduleCheckStudentDelayedAlertMessage message)
    {
      var job = CreateJob(message.BranchMediumId);
      var trigger = CreateTrigger(message.BranchMediumId, message.ScheduleTimestamp);
      await _scheduler.ScheduleJob(job, trigger);
    }

    private static IJobDetail CreateJob(long branchMediumId)
    {
      var jobName = $"SendCheckStudentDelayedAlertMessageJob#{branchMediumId}";
      var jobGroup = "SendCheckStudentDelayedAlertMessageJobGroup";
      return JobBuilder.Create<SendCheckStudentDelayedAlertMessageJob>()
        .WithIdentity(jobName, jobGroup)
        .UsingJobData(Constants.BranchMediumId, branchMediumId)
        .Build();
    }

    private static ITrigger CreateTrigger(long branchMediumId, DateTime scheduleTimestamp)
    {
      var triggerName = $"SendCheckStudentDelayedAlertMessageTrigger#{branchMediumId}";
      var triggerGroup = "SendCheckStudentDelayedAlertMessageTriggerGroup";
      var triggerBuilder = TriggerBuilder.Create().WithIdentity(triggerName, triggerGroup);
      var trigger = triggerBuilder
        .StartAt(scheduleTimestamp)
        .WithSimpleSchedule(x => x.WithMisfireHandlingInstructionFireNow())
        .Build();
      return trigger;
    }
  }
}