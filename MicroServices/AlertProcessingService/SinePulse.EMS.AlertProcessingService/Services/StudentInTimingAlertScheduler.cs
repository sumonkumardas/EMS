using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.AlertProcessingService.Utility;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Notification;
using SinePulse.EMS.Messages.ScheduleJobMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public class StudentInTimingAlertScheduler : IStudentInTimingAlertScheduler
  {
    private readonly IRepository _repository;
    private readonly IMessageSender _messageSender;

    public StudentInTimingAlertScheduler(IRepository repository, IMessageSender messageSender)
    {
      _repository = repository;
      _messageSender = messageSender;
    }

    public async Task ScheduleStudentInTimingAlert(DateTime scheduleDate)
    {
      var notificationConfigurations = await _repository.Filter<NotificationConfiguration, BranchMedium, Shift>(
        x => true,
        x => x.BranchMedium,
        x => x.BranchMedium.Shift).ToListAsync();

      foreach (var notificationConfiguration in notificationConfigurations)
      {
        var branchMedium = notificationConfiguration.BranchMedium;

        var safetyTimeSpan = TimeSpanUtility.ToTimeSpan(notificationConfiguration.EntryDelayPeriod,
          notificationConfiguration.EntryDelayTimeIntervalType);
        var delayCheckingTimestamp = scheduleDate + branchMedium.Shift.StartTime + safetyTimeSpan;
        await SendScheduleCheckStudentDelayedAlertMessage(branchMedium.Id, delayCheckingTimestamp);

        var absentTimeSpan = TimeSpanUtility.ToTimeSpan(notificationConfiguration.AbsentPeriod,
          notificationConfiguration.AbsentTimeIntervalType);
        var absentCheckingTimestamp = scheduleDate + branchMedium.Shift.StartTime + safetyTimeSpan + absentTimeSpan;
        await SendScheduleCheckStudentAbsentAlertMessage(branchMedium.Id, absentCheckingTimestamp);
      }
    }

    private async Task SendScheduleCheckStudentDelayedAlertMessage(long branchMediumId,
      DateTime delayCheckingTimestamp)
    {
      var message = new ScheduleCheckStudentDelayedAlertMessage
      {
        BranchMediumId = branchMediumId,
        ScheduleTimestamp = delayCheckingTimestamp
      };
      await _messageSender.Send(message, MicroServiceAddresses.ScheduleJobService);
    }

    private async Task SendScheduleCheckStudentAbsentAlertMessage(long branchMediumId,
      DateTime absentCheckingTimestamp)
    {
      var message = new ScheduleCheckStudentAbsentAlertMessage
      {
        BranchMediumId = branchMediumId,
        ScheduleTimestamp = absentCheckingTimestamp
      };
      await _messageSender.Send(message, MicroServiceAddresses.ScheduleJobService);
    }
  }
}