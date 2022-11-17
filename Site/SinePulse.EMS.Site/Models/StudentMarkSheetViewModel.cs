using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class StudentMarkSheetViewModel : BaseViewModel
  {
    public IEnumerable<SessionMessageModel> Sessions { get; set; }
    public long SessionId { get; set; }
    public long TermId { get; set; }
    public long BranchMediumId { get; set; }
    public long StudentId { get; set; }
  }
}