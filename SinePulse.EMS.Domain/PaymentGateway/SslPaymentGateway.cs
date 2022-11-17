using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.PaymentGateway
{
  public class SslPaymentGateway : BaseEntity
  {
    #region Primitive Properties

    public virtual string TransactionId { get; set; }
    public virtual string UserCode { get; set; }
    public virtual int Year { get; set; }
    public virtual MonthsOfYearEnum Month { get; set; }
    public virtual DateTime PaymentDate { get; set; }
    public virtual decimal PerStudentCharge { get; set; }
    public virtual int TotalStudents { get; set; }
    public virtual decimal TransactionAmount { get; set; }
    public virtual decimal StoreAmount { get; set; }
    public virtual string ValidationId { get; set; }
    public virtual string CardType { get; set; }

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