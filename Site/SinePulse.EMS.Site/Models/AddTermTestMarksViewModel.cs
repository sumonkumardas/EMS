namespace SinePulse.EMS.Site.Models
{
  public class AddTermTestMarksViewModel : BaseViewModel
  {
    public decimal FullMarks { get; set; }
    public decimal PassMarks { get; set; }
    public long TermId { get; set; }
    public long TermTestId { get; set; }
    public long[] StudentSectionIds { get; set; }
    public decimal[] ObtainedMarks { get; set; }
    public decimal[] GraceMarks { get; set; }
    public string[] RemarksArray { get; set; }
    public bool ActiveTestMarksAddTab { get; set; }

    public long ClassId { get; set; }
    public long Group { get; set; }
    public long SubjectId { get; set; }
    public long ExamType { get; set; }
    public long SectionId { get; set; }
    public new long BranchMediumId { get; set; }
  }
}