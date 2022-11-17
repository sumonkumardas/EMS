using SinePulse.EMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Models
{
  public class TransactionEntryViewModel : BaseViewModel
  {
    public long AccountHeadId { get; set; }
    public decimal DebitAmount { get; set; }
    public decimal CreditAmount { get; set; }
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public TransactionViewModel Transaction { get; set; }
    public BranchMediumAccountsHeadViewModel AccountHead { get; set; }
    }
}
