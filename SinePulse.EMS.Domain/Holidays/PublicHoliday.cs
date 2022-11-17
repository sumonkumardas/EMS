using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.Domain.Holidays
{
  public class PublicHoliday : BaseEntity
  {
    #region Primitive Properties
    public virtual string HolidayName { get; set; }
    public virtual DateTime StartDate { get; set; }
    public virtual DateTime EndDate { get; set; }
    public virtual StatusEnum Status { get; set; }
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
