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
  public class TermStartedAlertScheduler : ITermStartedAlertScheduler
  {
    private readonly IRepository _repository;
    private readonly IMessageSender _messageSender;

    public TermStartedAlertScheduler(IRepository repository, IMessageSender messageSender)
    {
      _repository = repository;
      _messageSender = messageSender;
    }

    public async Task ScheduleTermStartedAlert(long termId)
    {
      var term = await _repository.GetByIdAsync<ExamTerm, Session>(termId,
        x => x.Session);
      var branchMediumId = term.Session.BranchMediumId;
      var notificationConfiguration = await _repository
        .Filter<NotificationConfiguration>(x => x.BranchMedium.Id == branchMediumId)
        .FirstOrDefaultAsync();
      if (notificationConfiguration == null) return;

      var timeSpan = TimeSpanUtility.ToTimeSpan(notificationConfiguration.ExamStartPeriod,
        notificationConfiguration.ExamStartTimeIntervalType);

      await SendScheduleTermStartedAlertMessage(termId, term.StartDate - timeSpan);
    }

    private async Task SendScheduleTermStartedAlertMessage(long termId, DateTime scheduleTimestamp)
    {
      var message = new ScheduleTermStartedAlertMessage
      {
        TermId = termId,
        ScheduleTimestamp = scheduleTimestamp
      };
      await _messageSender.Send(message, MicroServiceAddresses.ScheduleJobService);
    }
  }
}