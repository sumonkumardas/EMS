using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Accounts
{
  public class AccountType : BaseEntity
  {
    #region Primitive Properties

    public virtual ChartOfAccountTypeEnum AccountTypeName { get; set; }
    public virtual FinancialStatementsEnum FinancialStatement { get; set; }
    public virtual AccountTransactionTypeEnum TransactionType { get; set; }
    public virtual int CodingStartValue { get; set; }
    public virtual int CodingEndValue { get; set; }

    #endregion

    #region Complex Properties

    private AuditFields _auditFields = new AuditFields();

    public virtual AuditFields AuditFields
    {
      get => _auditFields;
      set => _auditFields = value;
    }

    #endregion
  }
}