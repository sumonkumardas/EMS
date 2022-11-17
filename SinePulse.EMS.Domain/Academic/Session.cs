using System;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Academic
{
  public class Session : BaseEntity
  {
    public override string Title()
    {
      return this.SessionName + " (" + this.StartDate.ToShortDateString() + " - " + this.EndDate.ToShortDateString() + ")";
    }

    #region Primitive Properties

    public virtual string SessionName { get; set; }
    public virtual DateTime StartDate { get; set; }
    public virtual DateTime EndDate { get; set; }
    public virtual bool IsSessionClosing { get; set; }
    public virtual bool IsSessionClosed { get; set; }
    public virtual long BranchMediumId { get; set; }
    public virtual ObjectTypeEnum SessionType { get; set; }
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

    public virtual Institute Institute { get; set; }
    public virtual Branch Branch { get; set; }
    public virtual BranchMedium BranchMedium { get; set; }

    #endregion
  }
}