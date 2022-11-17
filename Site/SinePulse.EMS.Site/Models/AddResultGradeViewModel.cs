namespace SinePulse.EMS.Site.Models
{
  public class AddResultGradeViewModel : BaseViewModel
  {
    public string GradeLetter { get; set; }
    public decimal GradePoint { get; set; }
    public int StartMark { get; set; }
    public int EndMark { get; set; }
    public long BranchMediumId { get; set; }
  }
}