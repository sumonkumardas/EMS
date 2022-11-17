using System.Collections.Generic;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Banks
{
  public class Bank : BaseEntity
  {
    public override string Title()
    {
      return this.BankName;
    }

    #region Primitive Properties

    public virtual string BankName { get; set; }

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

    public virtual BranchMedium BranchMedium { get; set; }

    #endregion
    
    #region Collection Properties

    public virtual  ICollection<BankBranch> BankBranches { get; set; }

    #endregion
  }
}