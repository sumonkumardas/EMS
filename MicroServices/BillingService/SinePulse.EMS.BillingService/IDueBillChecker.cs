using System;
using System.Collections.Generic;
using System.Text;
using SinePulse.EMS.BillingService.Model;

namespace SinePulse.EMS.BillingService
{
  public interface IDueBillChecker
  {
    List<DueBillOfBranchMedium> GetDueBillsOfBranchMedium();
  }
}
