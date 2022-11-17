using System;
using System.ComponentModel.DataAnnotations;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Domain.Academic
{
  public class BreakTime : BaseEntity
  {
    #region Primitive Properties
    public virtual TimeSpan StartTime { get; set; }
    public virtual TimeSpan EndTime { get; set; }
    public virtual StatusEnum Status { get; set; }

    #endregion

    #region Complex Properties

    private AuditFields _auditFields = new AuditFields();

    [Required]
    public virtual AuditFields AuditFields
    {
      get { return _auditFields; }
      set { _auditFields = value; }
    }

    #endregion

    #region  Navigation Properties

    public virtual BranchMedium BranchMedium { get; set; }
    public virtual Session Session { get; set; }

    #endregion
  }
}