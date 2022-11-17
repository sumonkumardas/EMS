using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Students
{
  public class ContactInfo : BaseEntity
  {
    #region Primitive Properties

    public virtual string PhoneNo { get; set; }
    public virtual string EmailAddress { get; set; }
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

    public virtual Address PresentAddress { get; set; }
    public virtual Address PermanentAddress { get; set; }

    #endregion
  }
}