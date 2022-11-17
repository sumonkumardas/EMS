using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace SinePulse.EMS.Domain.Employees
{
  public class WorkingShift : BaseEntity
  {
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
  }
}
