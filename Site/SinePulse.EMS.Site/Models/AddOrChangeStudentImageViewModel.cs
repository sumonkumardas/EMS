namespace SinePulse.EMS.Site.Models
{
  public class AddOrChangeStudentImageViewModel : BaseViewModel
  {
    public long BranchMediumId { get; set; }
    public long StudentId { get; set; }
    public string ImageUrl { get; set; }
  }
}