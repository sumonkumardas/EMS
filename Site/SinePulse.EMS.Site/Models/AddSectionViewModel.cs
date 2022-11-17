using System.Collections.Generic;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class AddSectionViewModel : BaseViewModel
  {
    public long BranchMediumId { get; set; }
    public GroupEnum Group { get; set; }
    public string SectionName { get; set; }
    public int NumberOfStudents { get; set; }
    public int TotalClasses { get; set; }
    public int DurationOfClass { get; set; }
    public StatusEnum Status { get; set; }
    public long ClassId { get; set; }
    public long SessionId { get; set; }
    public IEnumerable<ClassMessageModel> Classes { get; set; }
    public IEnumerable<SessionMessageModel> Sessions { get; set; } = new List<SessionMessageModel>();
    public IEnumerable<ClassTest> ClassTests { get; set; } = new List<ClassTest>();
  }
}