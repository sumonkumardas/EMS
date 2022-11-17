using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Notification;
using SinePulse.EMS.Messages.NotificationMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.UseCases.Notifications;

namespace SinePulse.EMS.UseCases.Notifications
{
  public class EditNotificationConfigurationUseCase : IEditNotificationConfigurationUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public EditNotificationConfigurationUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void UpdateNotificationConfiguration(EditNotificationConfigurationRequestMessage message)
    {
      var notificationConfiguration = _repository.GetById<NotificationConfiguration>(message.ConfigurationId);

      var branchMedium = _repository.GetById<BranchMedium>(message.BranchMediumId);

      notificationConfiguration.AbsentPeriod = message.AbsentPeriod;
      notificationConfiguration.BranchMedium = branchMedium;
      notificationConfiguration.ClassTestStartPeriod = message.ClassTestStartPeriod;
      notificationConfiguration.EntryDelayPeriod = message.EntryDelayPeriod;
      notificationConfiguration.ExamStartPeriod = message.ExamStartPeriod;
      notificationConfiguration.ResultGradePreparePeriod = message.ResultGradePreparePeriod;
      notificationConfiguration.TermTestStartPeriod = message.TermTestStartPeriod;
      notificationConfiguration.Status = StatusEnum.Active;

      notificationConfiguration.AuditFields.LastUpdatedBy = message.CurrentUserName;
      notificationConfiguration.AuditFields.LastUpdatedDateTime = DateTime.Now;
      _dbContext.SaveChanges();
    }
  }
}