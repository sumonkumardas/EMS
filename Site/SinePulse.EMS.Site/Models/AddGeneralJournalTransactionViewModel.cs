using System;
using System.Collections.Generic;

namespace SinePulse.EMS.Site.Models
{
  public class AddGeneralJournalTransactionViewModel : BaseViewModel
  {
    public long BranchMediumId { get; set; }
    public string TransactionDate { get; set; }
    public string Description { get; set; }
    public long SessionId { get; set; }
    public List<TransactionEntryViewModel> TransactionEntries { get; set; }    
  }
}