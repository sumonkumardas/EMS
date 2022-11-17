using SinePulse.EMS.Domain.Academic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Models
{
  public class ServiceChargeViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public decimal PerStudentCharge { get; set; }
    public int PaymentBufferPeriod { get; set; }
    public DateTime EffectiveDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string InstituteName { get; set; }
    public string BranchName { get; set; }
    public BranchMedium BranchMediumData { get; set; }

    public class BranchMedium
    {
      public long BranchMediumId { get; set; }
      public string BranchMediumName { get; set; }
    }
  }
}
