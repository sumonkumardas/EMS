using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.Model.Academic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Models
{
  public class AddServiceChargeViewModel : BaseViewModel
  {
    public long ServiceChargeId { get; set; }
    public long InstituteId { get; set; }
    public IEnumerable<Institute> Institutes { get; set; }
    public long BranchId { get; set; }
    public IEnumerable<BranchMessageModel> Branches { get; set; }
    public long BranchMediumId { get; set; }
    public IEnumerable<MediumMessageModel> BranchMediums { get; set; }
    public decimal PerStudentCharge { get; set; }
    public int PaymentBufferPeriod { get; set; }
    public DateTime EffectiveDate { get; set; }
  }
}
