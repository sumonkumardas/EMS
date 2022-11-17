using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Banks;

namespace SinePulse.EMS.Site.Models
{
  public class ShowStudentListViewModel : BaseViewModel
  {
    public long BranchMediumId { get; set; }
    public long SessionId { get; set; }
    public long SectionId { get; set; }
    public long ClassId { get; set; }
    public List<SessionMessageModel> Sessions { get; set; }  
    public List<StudentViewModel> Students { get; set; }
  }
}