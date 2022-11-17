using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Leaves
{
  public class LeaveType : BaseEntity
  {
    #region Primitive Properties

    public virtual string LeaveName { get; set; }
    public virtual int LeavesPerYear { get; set; }
    public virtual bool CanEmployeesApplyBeyondTheCurrentLeaveBalance { get; set; }
    public virtual bool WillLeaveCarriedForward { get; set; }
    public virtual int PercentageOfLeaveCarriedForward { get; set; }
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
  }
}