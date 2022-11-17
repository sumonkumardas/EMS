using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Routines
{
  public class ClassRoutine : BaseEntity
  {
    #region Primitive Properties

    public virtual DayOfWeek WeekDay { get; set; }
    public virtual TimeSpan StartTime { get; set; }
    public virtual TimeSpan EndTime { get; set; }
    public virtual bool IsBreakTime { get; set; }
    public virtual StatusEnum Status { get; set; }

    #endregion

    #region Complex Properties
    private AuditFields _auditFields = new AuditFields();

    public virtual AuditFields AuditFields
    {
      get { return _auditFields; }
      set { _auditFields = value; }
    }
    #endregion

    #region  Navigation Properties

    public virtual Subject Subject { get; set; }
    public virtual Employee Teacher { get; set; }
    public virtual Section Section { get; set; }
    public virtual Room Room { get; set; }

    #endregion
  }
}
