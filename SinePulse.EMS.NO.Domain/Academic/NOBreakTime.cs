using NakedObjects;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Shared;
using SinePulse.EMS.ProjectPrimitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Academic
{
  [Table("BreakTimes")]
  [DisplayName("Break Time")]
  public class NOBreakTime : NOBaseEntity
  {
    #region Injected Services
    public IDomainObjectContainer Container { set; protected get; }
    #endregion

    #region Life Cycle Methods
    public virtual void Persisting()
    {
      AuditFields.InsertedBy = Container.Principal.Identity.Name;
      AuditFields.InsertedDateTime = DateTime.Now;
      AuditFields.LastUpdatedBy = Container.Principal.Identity.Name;
      AuditFields.LastUpdatedDateTime = DateTime.Now;
    }
    public virtual void Updating()
    {
      AuditFields.LastUpdatedBy = Container.Principal.Identity.Name;
      AuditFields.LastUpdatedDateTime = DateTime.Now;
    }
    #endregion

    #region Primitive Properties
    [Required, MemberOrder(10)]
    public virtual WeekDays WeekDays { get; set; }
    [Required, MemberOrder(20)]
    public virtual TimeSpan StartTime { get; set; }
    [MemberOrder(30), Required]
    public virtual TimeSpan EndTime { get; set; }
    [MemberOrder(580), Required]
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
    public bool HideAuditFields()
    {
      return true;
    }
    #endregion

    #region  Navigation Properties
    [Required, MemberOrder(1)]
    [DisplayName("Branch")]
    public virtual BranchMedium Branch { get; set; }
    [Required, MemberOrder(2)]
    [DisplayName("Session")]
    public virtual NOSession Session { get; set; }
    #endregion
  }
}
