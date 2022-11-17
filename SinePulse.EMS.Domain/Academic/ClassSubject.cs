using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Academic
{
  public class ClassSubject : BaseEntity
  {
    public override string Title()
    {
      return $"{Class.ClassName} - {Group.ToString()} - {Subject.SubjectName}";
    }

    #region Primitive Properties
    public virtual GroupEnum Group { get; set; }
    public virtual bool IsOptional { get; set; }
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
    public virtual Class Class { get; set; }
    public virtual Subject Subject { get; set; }
    #endregion
  }
}