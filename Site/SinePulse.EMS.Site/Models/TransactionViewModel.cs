using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Models
{
  public class TransactionViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public string TransactionNo { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; }
    public ICollection<TransactionEntryViewModel> TransactionEntries { get; set; }
  }
}
