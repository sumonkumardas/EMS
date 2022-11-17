using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Students
{
    public class ContactPerson:BaseEntity
    {
    #region Primitive Properties
    public virtual RelationTypeEnum RelationType { get; set; }
    public virtual string Name { get; set; }
    public virtual string PhoneNo { get; set; }
    public virtual string EmailAddress { get; set; }
    public virtual string NID { get; set; }
    public virtual string Profession { get; set; }
    public virtual string Designation { get; set; }
    public virtual string OfficeNameAddress { get; set; }
    public virtual string OfficeContactNo { get; set; }
    public virtual StatusEnum Status { get; set; }
    public virtual string ImageUrl { get; set; }
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
    public virtual Student Student { get; set; }
    public virtual Address PresentAddress { get; set; }
    public virtual Address PermanentAddress { get; set; }
    #endregion
  }
}
