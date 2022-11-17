using System;
using System.Collections.Generic;
using System.Text;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Billing
{
  public class PendingBillInfo:BaseEntity
  {
    #region Primitive Properties
    public virtual int Year { get; set; }
    public virtual MonthsOfYearEnum Month { get; set; }
    public virtual DateTime PaymentDuetDate { get; set; }
    public virtual decimal PerStudentCharge { get; set; }
    public virtual int TotalStudents { get; set; }
    public virtual decimal Amount { get; set; }
    public virtual decimal Vat { get; set; }
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

    public virtual BranchMedium BranchMedium { get; set; }

    #endregion
  }
}
