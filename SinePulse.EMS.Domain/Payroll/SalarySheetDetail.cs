using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Payroll
{
  public class SalarySheetDetail : BaseEntity
  {
    #region Primitive Properties

    public virtual decimal Amount { get; set; }
    public virtual string BankAccountNo { get; set; }

    #endregion

    #region Navigation Properties

    public virtual PayrollComponent PayrollComponent { get; set; }
    public virtual Employee Employee { get; set; }
    public virtual SalarySheet SalarySheet { get; set; }

    #endregion
  }
}