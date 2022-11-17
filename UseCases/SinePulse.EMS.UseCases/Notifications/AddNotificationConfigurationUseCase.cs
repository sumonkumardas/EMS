using System;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Notification;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.NotificationMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Notifications
{
  public class AddNotificationConfigurationUseCase : IAddNotificationConfigurationUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;
    private readonly MapperConfiguration _autoMapperConfig;

    public AddNotificationConfigurationUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public long AddNotificationConfiguration(AddNotificationConfigurationRequestMessage request)
    {
      var branchMedium = _repository.GetById<BranchMedium>(request.BranchMediumId);
      var notificationConfiguration = new NotificationConfiguration();

      notificationConfiguration = _repository.Table<NotificationConfiguration>(new[]
      {
        nameof(NotificationConfiguration.BranchMedium)
      }).FirstOrDefault(x => x.BranchMedium.Id == request.BranchMediumId);

      if (notificationConfiguration == null)
      {
        notificationConfiguration = new NotificationConfiguration()
        {
          BranchMedium = branchMedium,
          AbsentPeriod = request.AbsentPeriod,
          AbsentTimeIntervalType = request.AbsentTimeIntervalType,
          ClassTestStartPeriod = request.ClassTestStartPeriod,
          ClassTestStartTimeIntervalType = request.ClassTestStartTimeIntervalType,
          EntryDelayPeriod = request.EntryDelayPeriod,
          EntryDelayTimeIntervalType = request.EntryDelayTimeIntervalType,
          ExamStartPeriod = request.ExamStartPeriod,
          ExamStartTimeIntervalType = request.ExamStartTimeIntervalType,
          ResultGradePreparePeriod = request.ResultGradePreparePeriod,
          ResultGradePrepareTimeIntervalType = request.ResultGradePrepareTimeIntervalType,
          TermTestStartPeriod = request.TermTestStartPeriod,
          TermTestStartTimeIntervalType = request.TermTestStartTimeIntervalType,
          Status = StatusEnum.Active,

          AuditFields = new AuditFields
          {
            InsertedBy = request.CurrentUserName,
            InsertedDateTime = DateTime.Now,
            LastUpdatedBy = request.CurrentUserName,
            LastUpdatedDateTime = DateTime.Now
          }
        };
        _repository.Insert(notificationConfiguration);
      }
      else
      {

        notificationConfiguration.BranchMedium = branchMedium;
        notificationConfiguration.AbsentPeriod = request.AbsentPeriod;
        notificationConfiguration.AbsentTimeIntervalType = request.AbsentTimeIntervalType;
        notificationConfiguration.ClassTestStartPeriod = request.ClassTestStartPeriod;
        notificationConfiguration.ClassTestStartTimeIntervalType = request.ClassTestStartTimeIntervalType;
        notificationConfiguration.EntryDelayPeriod = request.EntryDelayPeriod;
        notificationConfiguration.EntryDelayTimeIntervalType = request.EntryDelayTimeIntervalType;
        notificationConfiguration.ExamStartPeriod = request.ExamStartPeriod;
        notificationConfiguration.ExamStartTimeIntervalType = request.ExamStartTimeIntervalType;
        notificationConfiguration.ResultGradePreparePeriod = request.ResultGradePreparePeriod;
        notificationConfiguration.ResultGradePrepareTimeIntervalType = request.ResultGradePrepareTimeIntervalType;
        notificationConfiguration.TermTestStartPeriod = request.TermTestStartPeriod;
        notificationConfiguration.TermTestStartTimeIntervalType = request.TermTestStartTimeIntervalType;
        notificationConfiguration.Status = StatusEnum.Active;
        notificationConfiguration.AuditFields.LastUpdatedBy = request.CurrentUserName;
        notificationConfiguration.AuditFields.LastUpdatedDateTime=DateTime.Now;
      }


      _dbContext.SaveChanges();

      return notificationConfiguration.Id;
    }
  }
}