using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class TrialBalanceViewModel:BaseViewModel
  {
    public long BranchMediumId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public BranchMedium Branch { get; set; }
    public List<AccountHead> AccountHeads { get; set; }
    public string AccountTypeTreeUi { get; set; }
    public string DebitTreeUi { get; set; }
    public string CreditTreeUi { get; set; }
    public string EmptyTreeUi { get; set; }
    public string TotalDebit { get; set; }
    public string TotalCredit { get; set; }
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
