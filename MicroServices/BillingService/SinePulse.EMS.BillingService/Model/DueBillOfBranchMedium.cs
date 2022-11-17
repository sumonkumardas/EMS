using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.BillingService.Model
{
  public class DueBillOfBranchMedium
  {
    public string Email { get; set; }
    public decimal DueBill { get; set; }
    public string Month { get; set; }
    public string Year { get; set; }
    public string MobileNo { get; set; }
    public long BranchMediumId { get; set; }
    public string BranchMediumName { get; set; }

  }
}
