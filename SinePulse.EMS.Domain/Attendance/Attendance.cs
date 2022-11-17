using System;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;

namespace SinePulse.EMS.Domain.Attendance
{
  public class Attendance : BaseEntity
  {
    #region Primitive Properties

    public virtual DateTime AttendanceDate { get; set; }
    public virtual TimeSpan? InTime { get; set; }
    public virtual TimeSpan? OutTime { get; set; }

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
    public virtual Employee Employee { get; set; }
    public virtual Student Student { get; set; }
    #endregion
  }
}