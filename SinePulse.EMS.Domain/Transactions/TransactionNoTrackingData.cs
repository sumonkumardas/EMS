using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Transactions
{
  public class TransactionNoTrackingData : BaseEntity
  {
    public override string Title()
    {
      return $"{TransactionPrefix}{NextTransactionNoCounter}";
    }

    #region Primitive Properties

    public virtual int NextTransactionNoCounter { get; set; }
    public virtual string TransactionPrefix { get; set; }
    public virtual long BranchMediumId { get; set; }

    #endregion

    #region Complex Properties

    public virtual AuditFields AuditFields { get; set; } = new AuditFields();

    #endregion

    #region Navigation Properties

    public virtual BranchMedium BranchMedium { get; set; }

    #endregion
  }
}