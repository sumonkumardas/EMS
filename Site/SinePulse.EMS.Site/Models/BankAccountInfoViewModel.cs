using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Models
{
  public class BankAccountInfoViewModel : BaseViewModel
  {
    public long BankId { get; set; }
    public long BankBranchId { get; set; }
    public string BankName { get; set; }
    public string BankBranchName { get; set; }
    public string AccountNumber { get; set; }
  }
}
