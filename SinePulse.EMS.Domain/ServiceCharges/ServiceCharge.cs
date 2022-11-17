using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.Domain.ServiceCharges
{
  public class ServiceCharge : BaseEntity
  {
    #region Primitive Properties
    public virtual decimal PerStudentCharge { get; set; }
    public virtual int PaymentBufferPeriod { get; set; }
    public virtual DateTime EffectiveDate { get; set; }
    public virtual DateTime? EndDate { get; set; }
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
