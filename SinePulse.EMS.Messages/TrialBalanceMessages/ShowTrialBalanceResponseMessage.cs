using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.TrialBalanceMessages
{
  public class ShowTrialBalanceResponseMessage
  {
    public List<AccountHead> AccountHeads { get; set; }

    public decimal TotalDebit { get; set; }

    public decimal TotalCredit { get; set; }

    public class AccountHead
    {
      public long AccountHeadId { get; set; }

      public string AccountHeadName { get; set; }

      public decimal Balance { get; set; }

      public decimal Debit { get; set; }

      public decimal Credit { get; set; }

      public AccountTransactionTypeEnum TransactionEntryType { get; set; }

      public List<AccountHead> AccountHeads { get; set; }
    }
  }
}