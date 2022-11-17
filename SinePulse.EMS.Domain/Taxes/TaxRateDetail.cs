using SinePulse.EMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.Domain.Taxes
{
  public class TaxRateDetail: BaseEntity
  {
    #region Primitive Properties
    public virtual int LimitFrom { get; set; }
    public virtual int? LimitTo { get; set; }
    public virtual decimal Percentage { get; set; }
    #endregion

    #region Navigation Properties
    public virtual TaxRate TaxRate { get; set; }
    #endregion
  }
}
