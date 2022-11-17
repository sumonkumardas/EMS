using NakedObjects;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Shared;
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
  [Table("Sessions")]
  [DisplayName("Session")]
  [Bounded]
  public class NOSession : NOBaseEntity
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
    [Required, MemberOrder(10), Title]
    public virtual string SessionName { get; set; }
    [Required, MemberOrder(20)]
    [Mask("d")]
    public virtual DateTime StartDate { get; set;}
    [Required, MemberOrder(30)]
    [Mask("d")]
    public virtual DateTime EndDate { get; set; }
    [Required, MemberOrder(40)]
    public virtual bool IsSessionClosed { get; set; }
    [Required, MemberOrder(50)]
    public virtual ObjectTypeEnum SessionType { get; set; }
    [Required, MemberOrder(60)]
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

    #region Behavior
    public void SetAsCurrentSession()
    {
      this.IsSessionClosed = true;
    }
    #endregion
  }
}
