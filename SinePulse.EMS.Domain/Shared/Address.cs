using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.Domain.Shared
{
  public class Address : BaseEntity
  {
    #region Primitive Properties
    public virtual string Street1 { get; set; }
    public virtual string Street2 { get; set; }
    public virtual string PostalCode { get; set; }
    public virtual string City { get; set; }
    #endregion

    #region Complex Properties

    #region AuditFields (AuditFields)

    private AuditFields _auditFields = new AuditFields();
    public virtual AuditFields AuditFields
    {
      get { return _auditFields; }
      set { _auditFields = value; }
    }
    #endregion

    #endregion
  }
}
