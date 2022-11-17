using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.ManagingCommittee
{
  public class CommitteeMember : BaseEntity
  {
    public override string Title()
    {
      return FullName + " (" + MobileNo + ")";
    }

    #region Primitive Properties

    public virtual string FullName { get; set; }
    public virtual string NationalId { get; set; }
    public virtual DateTime? DOB { get; set; }
    public virtual string MobileNo { get; set; }
    public virtual string EmailAddress { get; set; }
    public virtual string ImageUrl { get; set; }
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

    #region Navigation Properties

    public virtual Institute Institute { get; set; }
    public virtual Designation Designation { get; set; }
    public virtual Address PresentAddress { get; set; }
    public virtual Address PermanentAddress { get; set; }

    #endregion
  }
}