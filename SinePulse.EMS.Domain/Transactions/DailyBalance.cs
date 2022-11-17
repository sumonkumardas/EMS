using System;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Transactions
{
  public class DailyBalance : BaseEntity
  {
    #region Primitive Properties

    public virtual decimal BalanceAmount { get; set; }
    public virtual DateTime Date { get; set; }

    public virtual long AccountHeadId { get; set; }

    #endregion

    #region Navigational Properties

    public virtual BranchMediumAccountHead AccountHead { get; set; }

    #endregion
  }
}