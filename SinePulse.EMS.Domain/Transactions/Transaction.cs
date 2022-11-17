using System;
using System.Collections.Generic;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Transactions
{
  public class Transaction : BaseEntity
  {
    public override string Title()
    {
      return TransactionNo;
    }

    #region Primitive Properties

    public virtual string TransactionNo { get; set; }
    public virtual DateTime TransactionDate { get; set; }
    public virtual string Description { get; set; }

    #endregion

    #region Complex Properties

    public virtual AuditFields AuditFields { get; set; } = new AuditFields();

    #endregion

    #region  Collection Properties

    public virtual ICollection<TransactionEntry> TransactionEntries { get; set; }

    #endregion
  }
}