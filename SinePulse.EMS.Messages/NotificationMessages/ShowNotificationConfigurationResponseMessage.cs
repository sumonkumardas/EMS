using System;
using FluentValidation.Results;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.NotificationMessages
{
  public class ShowNotificationConfigurationResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public NotificationConfiguration NotificationConfigurationModel { get; }

    public ShowNotificationConfigurationResponseMessage(ValidationResult validationResult, NotificationConfiguration notificationConfiguration)
    {
      ValidationResult = validationResult;
      NotificationConfigurationModel = notificationConfiguration;
    }

    public class NotificationConfiguration 
    {
      public long Id { get; set; }
      public  int EntryDelayPeriod { get; set; }
      public  int AbsentPeriod { get; set; }
      public  int ExamStartPeriod { get; set; }
      public  int ClassTestStartPeriod { get; set; }
      public  int TermTestStartPeriod { get; set; }
      public  int ResultGradePreparePeriod { get; set; }
      public TimeIntervalType EntryDelayTimeIntervalType { get; set; }
      public TimeIntervalType AbsentTimeIntervalType { get; set; }
      public TimeIntervalType ExamStartTimeIntervalType { get; set; }
      public TimeIntervalType ClassTestStartTimeIntervalType { get; set; }
      public TimeIntervalType TermTestStartTimeIntervalType { get; set; }
      public TimeIntervalType ResultGradePrepareTimeIntervalType { get; set; }
      public  StatusEnum Status { get; set; }
      public BranchMedium BranchMedium { get; set; }
    }

    public class BranchMedium
    {
      public long Id { get; set; }
      public string BranchMediumName { get; set; }
    }
  }
}