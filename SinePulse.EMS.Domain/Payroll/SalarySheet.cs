using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Transactions;

namespace SinePulse.EMS.Domain.Payroll
{
  public class SalarySheet : BaseEntity
  {
    #region Primitive Properties

    public virtual DateTime GenerationDate { get; set; }
    public virtual int Year { get; set; }
    public virtual int Month { get; set; }
    public virtual decimal TotalSalary { get; set; }
    public virtual decimal TotalTaxes { get; set; }
    public virtual decimal TotalOtherDeduction { get; set; }
    public virtual string BankAccountNo { get; set; }
    public virtual bool IsAccountPosted { get; set; }

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

    public virtual BranchMedium BranchMedium { get; set; }
    public virtual Transaction Transaction { get; set; }

    #endregion
  }
}