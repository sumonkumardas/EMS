using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SinePulse.EMS.Domain.Academic
{
  public class Shift : BaseEntity
  {
    public override string Title()
    {
      return this.ShiftName + " (" + this.StartTime.ToString() + " - " + this.EndTime.ToString() + ")";
    }

    #region Primitive Properties
    public virtual string ShiftName { get; set; }
    public virtual TimeSpan StartTime { get; set; }
    public virtual TimeSpan EndTime { get; set; }
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

    #region  Navigation Properties

    public virtual Branch Branch { get; set; }

    #endregion

  }
}
