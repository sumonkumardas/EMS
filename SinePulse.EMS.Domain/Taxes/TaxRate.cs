using SinePulse.EMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.Domain.Taxes
{
  public class TaxRate: BaseEntity
  {
    #region Primitive Properties
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

    #region Collection Properties
    public virtual ICollection<TaxRateDetail> TaxRateDetails { get; set; } = new List<TaxRateDetail>();
    #endregion
  }
}
