using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Employees
{
  public class Grade : BaseEntity
  {
    #region Primitive Properties

    public virtual string GradeTitle { get; set; }
    public virtual decimal MinSalary { get; set; }
    public virtual decimal MaxSalary { get; set; }
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
  }
}