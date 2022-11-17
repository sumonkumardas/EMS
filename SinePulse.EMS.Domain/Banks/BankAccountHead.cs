using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Banks
{
  public class BankAccountHead : BaseEntity
  {
    #region Complex Properties

    private AuditFields _auditFields = new AuditFields();

    public virtual AuditFields AuditFields
    {
      get { return _auditFields; }
      set { _auditFields = value; }
    }

    #endregion

    #region  Navigation Properties

    public virtual Bank Bank { get; set; }
    public virtual BranchMediumAccountHead AccountHead { get; set; }

    #endregion
  }
}