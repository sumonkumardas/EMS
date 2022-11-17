using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Banks
{
  public class BankAccount : BaseEntity
  {
    #region Primitive Properties

    public virtual string AccountNumber { get; set; }
    public virtual BankAccountTypeEnum AccountType { get; set; }

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

    public virtual BankBranch BankBranch { get; set; }

    #endregion
  }
}