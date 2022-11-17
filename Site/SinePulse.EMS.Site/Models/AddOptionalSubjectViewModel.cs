using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.SubjectMessages;

namespace SinePulse.EMS.Site.Models
{
  public class AddOptionalSubjectViewModel : BaseViewModel
  {
    public long StudentId { get; set; }
    public long ClassId { get; set; }
    public GroupEnum Group { get; set; }
    public long[] OptionalSubjectIds { get; set; }
    public IEnumerable<GetOptionalSubjectListResponseMessage.Subject> OptionalSubjects { get; set; }
    public long BranchMediumId { get; set; }
    public string StudentName { get; set; }
    public string ClassName { get; set; }
    public long SectionId { get; set; }
    public string SectionName { get; set; }
    public int Roll { get; set; }
  }
}