using System.Collections.Generic;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Banks
{
  public class BankBranch : BaseEntity
  {
    #region Primitive Properties

    public virtual string BranchName { get; set; }
    public virtual string Address { get; set; }

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

    public virtual Bank Bank { get; set; }

    #endregion

    #region Collection Properties

    public ICollection<BankAccount> BankAccounts { get; set; }

    #endregion
  }
}