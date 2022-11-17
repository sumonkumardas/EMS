using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class AddAcademicFeeViewModel : BaseViewModel
  {
    public new long BranchMediumId { get; set; }
    public long SessionId { get; set; }
    public IEnumerable<SessionMessageModel> Sessions { get; set; }
    public long ClassId { get; set; }
    public IEnumerable<ClassMessageModel> Classes { get; set; }
    public AcademicFeePeriodEnum AcademicFeePeriod { get; set; }
    public decimal[] FeesCollection { get; set; }
  }
}