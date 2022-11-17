using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class AddWaiverViewModel : BaseViewModel
  {
    public new long BranchMediumId { get; set; }
    public long StudentId { get; set; }
    public string StudentName { get; set; }
    public int Roll { get; set; }
    public string SectionName { get; set; }
    public long SectionId { get; set; }
    public string ClassName { get; set; }
    public long ClassId { get; set; }
    public IEnumerable<SessionMessageModel> Sessions { get; set; }
    public long SessionId { get; set; }
    public AcademicFeePeriodEnum AcademicFeePeriod { get; set; }
    public decimal[] WaiverCollection { get; set; }
    public int[] InPercentage { get; set; }
  }
}