using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Accounts
{
  public class AccountTransactionType : BaseEntity
  {
    #region Primitive Properties

    public virtual AccountTransactionTypeEnum TransactionType { get; set; }
    public virtual IncreaseDecreaseEnum IncreaseDecreaseType { get; set; }

    #endregion

    #region Complex Properties

    private AuditFields _auditFields = new AuditFields();

    public virtual AuditFields AuditFields
    {
      get => _auditFields;
      set => _auditFields = value;
    }

    #endregion

    #region  Navigation Properties

    public virtual AccountType AccountType { get; set; }

    #endregion
  }
}