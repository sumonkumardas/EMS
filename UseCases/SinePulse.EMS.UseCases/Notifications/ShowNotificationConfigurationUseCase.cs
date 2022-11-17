using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Notification;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.NotificationMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Notifications
{
  public class ShowNotificationConfigurationUseCase : IShowNotificationConfigurationUseCase
  {
    private readonly IRepository _repository;

    public ShowNotificationConfigurationUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public ShowNotificationConfigurationResponseMessage.NotificationConfiguration ShowNotificationConfiguration(ShowNotificationConfigurationRequestMessage message)
    {
      var dbNotificationConfiguration = _repository.Table<NotificationConfiguration>(new[]
      {
        nameof(NotificationConfiguration.BranchMedium),
        nameof(NotificationConfiguration.BranchMedium)+"."+nameof(NotificationConfiguration.BranchMedium.Branch),
        nameof(NotificationConfiguration.BranchMedium)+"."+nameof(NotificationConfiguration.BranchMedium.Medium)
      }).FirstOrDefault(x => x.BranchMedium.Id == message.BranchMediumId);
      var branchMedium = _repository.GetByIdWithInclude<BranchMedium>(message.BranchMediumId, new[]
      {
        nameof(BranchMedium.Branch), nameof(BranchMedium.Medium)
      });
      var notificationConfiguration = new ShowNotificationConfigurationResponseMessage.NotificationConfiguration
      {
        BranchMedium = new ShowNotificationConfigurationResponseMessage.BranchMedium()
        {
          BranchMediumName = branchMedium.Branch.BranchName + "->" +
                             branchMedium.Medium.MediumName,
          Id = branchMedium.Id
        }
      };

      if (dbNotificationConfiguration == null) return notificationConfiguration;

      notificationConfiguration.Id = dbNotificationConfiguration.Id;
      notificationConfiguration.AbsentPeriod = dbNotificationConfiguration.AbsentPeriod;
      notificationConfiguration.AbsentTimeIntervalType = dbNotificationConfiguration.AbsentTimeIntervalType;
      notificationConfiguration.ClassTestStartPeriod = dbNotificationConfiguration.ClassTestStartPeriod;
      notificationConfiguration.ClassTestStartTimeIntervalType = dbNotificationConfiguration.ClassTestStartTimeIntervalType;
      notificationConfiguration.EntryDelayPeriod = dbNotificationConfiguration.EntryDelayPeriod;
      notificationConfiguration.EntryDelayTimeIntervalType = dbNotificationConfiguration.EntryDelayTimeIntervalType;
      notificationConfiguration.ExamStartPeriod = dbNotificationConfiguration.ExamStartPeriod;
      notificationConfiguration.ExamStartTimeIntervalType = dbNotificationConfiguration.ExamStartTimeIntervalType;
      notificationConfiguration.ResultGradePreparePeriod = dbNotificationConfiguration.ResultGradePreparePeriod;
      notificationConfiguration.ResultGradePrepareTimeIntervalType = dbNotificationConfiguration.ResultGradePrepareTimeIntervalType;
      notificationConfiguration.TermTestStartPeriod = dbNotificationConfiguration.TermTestStartPeriod;
      notificationConfiguration.TermTestStartTimeIntervalType = dbNotificationConfiguration.TermTestStartTimeIntervalType;
      notificationConfiguration.Status = dbNotificationConfiguration.Status;


      return notificationConfiguration;
    }
  }
}