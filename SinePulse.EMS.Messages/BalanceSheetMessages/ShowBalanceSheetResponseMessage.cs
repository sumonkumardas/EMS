using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.BalanceSheetMessages
{
  public class ShowBalanceSheetResponseMessage
  {
    public AccountHead AssetAccountHead { get; set; }

    public AccountHead LiabilitiesAccountHead { get; set; }

    public AccountHead EquityAccountHead { get; set; }

    public decimal TotalAsset { get; set; }

    public decimal TotalLiabilities { get; set; }

    public decimal TotalEquity { get; set; }

    public decimal NetIncome { get; set; }

    public bool IsYearClosing { get; set; }

    public class AccountHead
    {
      public long AccountHeadId { get; set; }

      public string AccountHeadName { get; set; }

      public decimal Amount { get; set; }

      public AccountTransactionTypeEnum TransactionEntryType { get; set; }

      public List<AccountHead> AccountHeads { get; set; }
    }
  }
}