using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Employees
{
  public class JobType : BaseEntity
  {
    #region Primitive Properties
    
    public virtual string JobTitle { get; set; }
    public virtual bool HasOverTime { get; set; }
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