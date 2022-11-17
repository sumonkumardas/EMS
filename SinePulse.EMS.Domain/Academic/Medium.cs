using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SinePulse.EMS.Domain.Academic
{
  public class Medium : BaseEntity
  {
    public override string Title()
    {
      return this.MediumName + " (" + this.MediumCode + ")";
    }

    #region Primitive Properties
    public virtual string MediumName { get; set; }
    public virtual string MediumCode { get; set; }
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
