using System;
using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class BalanceSheetViewModel : BaseViewModel
  {
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public BranchMedium Branch { get; set; }
    public AccountHead AssetAccountHead { get; set; }
    public AccountHead LiabilitiesAccountHead { get; set; }
    public AccountHead EquityAccountHead { get; set; }
    public string AccountTypeTreeUi { get; set; }
    public string DebitTreeUi { get; set; }
    public string CreditTreeUi { get; set; }
    public string EmptyTreeUi { get; set; }
    public string TotalAsset { get; set; }
    public string TotalLiabilities { get; set; }
    public string TotalEquity { get; set; }
    public string NetIncome { get; set; }
    public bool IsYearClosing { get; set; }
    public class AccountHead
    {
      public long AccountHeadId { get; set; }
      public string AccountHeadName { get; set; }
      public decimal Amount { get; set; }
      public AccountTransactionTypeEnum TransactionEntryType { get; set; }
      public List<AccountHead> ChildAccountHeads { get; set; }
    }
    public class BranchMedium
    {
      public long BranchMediumId { get; set; }
      public string BranchMediumName { get; set; }
    }
  }
}
