using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Models
{
  public class JournalTransactionResponseViewModel : BaseViewModel
  {
    public long BranchMediumId { get; set; }
    public bool IsDataPosted { get; set; }
    public List<string> ErrorMessages { get; set; }
    public string RedirectTo { get; set; }
  }
}
