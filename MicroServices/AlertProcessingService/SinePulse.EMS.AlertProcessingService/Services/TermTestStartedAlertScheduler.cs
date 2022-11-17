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
  public class TermTestStartedAlertScheduler : ITermTestStartedAlertScheduler
  {
    private readonly IRepository _repository;
    private readonly IMessageSender _messageSender;

    public TermTestStartedAlertScheduler(IRepository repository, IMessageSender messageSender)
    {
      _repository = repository;
      _messageSender = messageSender;
    }

    public async Task ScheduleTermTestStartedAlert(long termTestId)
    {
      var termTest = await _repository.GetByIdAsync<TermTest, Session>(termTestId,
        x => x.ExamConfiguration.Term.Session);
      var branchMediumId = termTest.ExamConfiguration.Term.Session.BranchMediumId;
      var notificationConfiguration = await _repository
        .Filter<NotificationConfiguration>(x => x.BranchMedium.Id == branchMediumId)
        .FirstOrDefaultAsync();
      if (notificationConfiguration == null) return;

      var timeSpan = TimeSpanUtility.ToTimeSpan(notificationConfiguration.TermTestStartPeriod,
        notificationConfiguration.TermTestStartTimeIntervalType);

      await SendScheduleTermTestStartedAlertMessage(termTestId, termTest.StartTimestamp - timeSpan);
    }

    private async Task SendScheduleTermTestStartedAlertMessage(long termTestId, DateTime scheduleTimestamp)
    {
      var message = new ScheduleTermTestStartedAlertMessage
      {
        TermTestId = termTestId,
        ScheduleTimestamp = scheduleTimestamp
      };
      await _messageSender.Send(message, MicroServiceAddresses.ScheduleJobService);
    }
  }
}