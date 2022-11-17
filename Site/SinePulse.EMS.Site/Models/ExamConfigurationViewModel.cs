using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class ExamConfigurationViewModel : BaseViewModel
  {
    public long ExamConfigurationId { get; set; }
    public int SubjectiveFullMark { get; set; }
    public int SubjectivePassMark { get; set; }
    public int ObjectiveFullMark { get; set; }
    public int ObjectivePassMark { get; set; }
    public int PracticalFullMark { get; set; }
    public int PracticalPassMark { get; set; }
    public int ClassTestPercentage { get; set; }
    public long TermId { get; set; }
    public string ClassName { get; set; }
    public string SubjectName { get; set; }
    public long ClassId { get; set; }
    public long SubjectId { get; set; }
    public long GroupId { get; set; }
    public StatusEnum Status { get; set; }
  }
}