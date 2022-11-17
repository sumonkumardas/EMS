using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class AddMarkViewModel : BaseViewModel
  {
    public decimal ObtainedMark { get; set; }
    public decimal GraceMark { get; set; }
    public string Comment { get; set; }
    public long TestId { get; set; }
    public long StudentSectionId { get; set; }
    public List<SessionMessageModel> Sessions { get; set; }
    public long SessionId { get; set; }
    public long TermId { get; set; }
    public long SubjectId { get; set; }
    public long BranchMediumId { get; set; }
    public ExamTypeEnum ExamType { get; set; }
  }
}