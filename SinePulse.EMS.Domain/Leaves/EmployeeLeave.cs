using System;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Leaves
{
  public class EmployeeLeave : BaseEntity
  {
    #region Primitive Properties

    public virtual DateTime StartDate { get; set; }
    public virtual DateTime EndDate { get; set; }
    public virtual string Reason { get; set; }
    public virtual LeaveStatusEnum LeaveStatus { get; set; }

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

    public virtual LeaveType LeaveType { get; set; }
    public virtual Employee Employee { get; set; }
    public virtual Employee ApproveBy { get; set; }

    #endregion
  }
}