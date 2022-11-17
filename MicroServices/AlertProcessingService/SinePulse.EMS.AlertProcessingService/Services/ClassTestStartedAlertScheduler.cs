using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.AlertProcessingService.Utility;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Notification;
using SinePulse.EMS.Messages.ScheduleJobMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public class ClassTestStartedAlertScheduler : IClassTestStartedAlertScheduler
  {
    private readonly IRepository _repository;
    private readonly IMessageSender _messageSender;

    public ClassTestStartedAlertScheduler(IRepository repository, IMessageSender messageSender)
    {
      _repository = repository;
      _messageSender = messageSender;
    }

    public async Task ScheduleClassTestStartedAlert(long classTestId)
    {
      var classTest = await _repository.GetByIdAsync<ClassTest, Session>(classTestId,
        x => x.ExamConfiguration.Term.Session);
      var branchMediumId = classTest.ExamConfiguration.Term.Session.BranchMediumId;
      var notificationConfiguration = await _repository
        .Filter<NotificationConfiguration>(x => x.BranchMedium.Id == branchMediumId)
        .FirstOrDefaultAsync();
      if (notificationConfiguration == null) return;

      var timeSpan = TimeSpanUtility.ToTimeSpan(notificationConfiguration.ClassTestStartPeriod,
        notificationConfiguration.ClassTestStartTimeIntervalType);

      await SendScheduleClassTestStartedAlertMessage(classTestId, classTest.StartTimestamp - timeSpan);
    }

    private async Task SendScheduleClassTestStartedAlertMessage(long classTestId, DateTime scheduleTimestamp)
    {
      var message = new ScheduleClassTestStartedAlertMessage
      {
        ClassTestId = classTestId,
        ScheduleTimestamp = scheduleTimestamp
      };
      await _messageSender.Send(message, MicroServiceAddresses.ScheduleJobService);
    }
  }
}