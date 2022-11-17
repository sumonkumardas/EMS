using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.Domain.Payroll
{
  public class EmployeeSalary: BaseEntity
  {
    #region Primitive Properties
    public virtual decimal Amount { get; set; }
    public virtual DateTime EffectiveDate { get; set; }
    #endregion

    #region Complex Properties
    private AuditFields _auditFields = new AuditFields();

    public virtual AuditFields AuditFields
    {
      get { return _auditFields; }
      set { _auditFields = value; }
    }
    #endregion

    #region Navigation Properties
    public virtual PayrollComponent PayrollComponent { get; set; }
    public virtual Employee Employee { get; set; }
    #endregion
  }
}
