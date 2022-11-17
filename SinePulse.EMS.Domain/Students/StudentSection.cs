using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Students
{
  public class StudentSection : BaseEntity
  {
    #region Primitive Properties

    public virtual int RollNo { get; set; }
    public GroupEnum Group { get; set; }
    public virtual long StudentId { get; set; }
    public virtual long ClassId { get; set; }

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
    public virtual Section Section { get; set; }
    public Class Class { get; set; }

    #endregion
  }
}