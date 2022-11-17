using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;

namespace SinePulse.EMS.Domain.Academic
{
  public class SubjectChoice : BaseEntity
  {
    #region Primitive Properties

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

    public virtual ClassSubject Subject { get; set; }
    public virtual Student Student { get; set; }
    public virtual Session Session { get; set; }

    #endregion
  }
}