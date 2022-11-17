using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Notification
{
  public class NotificationConfiguration : BaseEntity
  {
    #region Primitive Properties

    public virtual int EntryDelayPeriod { get; set; }
    public virtual TimeIntervalType EntryDelayTimeIntervalType { get; set; }
    public virtual int AbsentPeriod { get; set; }
    public virtual TimeIntervalType AbsentTimeIntervalType { get; set; }
    public virtual int ExamStartPeriod { get; set; }
    public virtual TimeIntervalType ExamStartTimeIntervalType { get; set; }
    public virtual int ClassTestStartPeriod { get; set; }
    public virtual TimeIntervalType ClassTestStartTimeIntervalType { get; set; }
    public virtual int TermTestStartPeriod { get; set; }
    public virtual TimeIntervalType TermTestStartTimeIntervalType { get; set; }
    public virtual int ResultGradePreparePeriod { get; set; }
    public virtual TimeIntervalType ResultGradePrepareTimeIntervalType { get; set; }
    public virtual StatusEnum Status { get; set; }

    #endregion

    #region Complex Properties

    public virtual AuditFields AuditFields { get; set; } = new AuditFields();

    #endregion

    #region  Navigation Properties

    public BranchMedium BranchMedium { get; set; }
    #endregion
  }
}
