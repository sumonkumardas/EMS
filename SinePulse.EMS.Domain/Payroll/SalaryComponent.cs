using SinePulse.EMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.Domain.Payroll
{
  public class SalaryComponent: PayrollComponent
  {
    #region Navigation Properties
    public virtual SalaryComponentType ComponentType { get; set; }
    #endregion
  }
}
