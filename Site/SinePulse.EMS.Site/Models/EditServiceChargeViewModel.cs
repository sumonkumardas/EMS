using SinePulse.EMS.Domain.Academic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Models
{
  public class EditServiceChargeViewModel : BaseViewModel
  {
    public long ServiceChargeId { get; set; }
    public decimal PerStudentCharge { get; set; }
    public int PaymentBufferPeriod { get; set; }
    public DateTime EffectiveDate { get; set; }
    public DateTime? EndDate { get; set; }
    public long BranchMediumId { get; set; }
    public BranchMedium BranchMedium { get; set; }
  }
}
