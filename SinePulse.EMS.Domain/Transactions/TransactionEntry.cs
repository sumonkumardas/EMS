using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Transactions
{
  public class TransactionEntry : BaseEntity
  {
    public override string Title()
    {
      return $"{Amount}/=";
    }

    #region Primitive Properties

    public virtual decimal Amount { get; set; }

    #endregion

    #region Complex Properties

    public virtual AuditFields AuditFields { get; set; } = new AuditFields();

    #endregion

    #region  Navigation Properties

    public virtual Transaction Transaction { get; set; }
    public virtual BranchMediumAccountHead AccountHead { get; set; }

    #endregion
  }
}