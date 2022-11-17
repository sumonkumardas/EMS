using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Payroll
{
  public class PayrollComponent : BaseEntity
  {
    #region Primitive Properties

    public virtual string ComponentName { get; set; }
    public virtual IncreaseDecreaseEnum IncreaseDecrease { get; set; }

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