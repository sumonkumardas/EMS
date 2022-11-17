using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Site.Models
{
  public class ShowTransactionViewModel : BaseViewModel
  {
    public long TransactionBranchMediumId { get; set; }
    public decimal TotalDebit { get; set; }
    public decimal TotalCredit { get; set; }
    public TransactionViewModel TransactionViewModel { get; set; }
  }
}
