using System;
using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.NotificationMessages
{
  public class EditNotificationConfigurationRequestMessage : IRequest<EditNotificationConfigurationResponseMessage>
  {
    public int EntryDelayPeriod { get; set; }
    public int AbsentPeriod { get; set; }
    public int ExamStartPeriod { get; set; }
    public int ClassTestStartPeriod { get; set; }
    public int TermTestStartPeriod { get; set; }
    public int ResultGradePreparePeriod { get; set; }
    public long ConfigurationId { get; set; }
    public long BranchMediumId { get; set; }
    public string CurrentUserName { get; set; }
    public TimeIntervalType EntryDelayTimeIntervalType { get; set; }
    public TimeIntervalType AbsentTimeIntervalType { get; set; }
    public TimeIntervalType ExamStartTimeIntervalType { get; set; }
    public TimeIntervalType ClassTestStartTimeIntervalType { get; set; }
    public TimeIntervalType TermTestStartTimeIntervalType { get; set; }
    public TimeIntervalType ResultGradePrepareTimeIntervalType { get; set; }
    public StatusEnum Status { get; set; }
  }
}