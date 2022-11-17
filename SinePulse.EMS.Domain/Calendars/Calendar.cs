using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SinePulse.EMS.Domain.Calendars
{
  public class Calendar : BaseEntity
  {
    #region Primitive Properties
    public virtual string Description { get; set; }
    public virtual DateTime StartDate { get; set; }
    public virtual DateTime EndDate { get; set; }
    public virtual CalendarTypeEnum CalendarType { get; set; }
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
