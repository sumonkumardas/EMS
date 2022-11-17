using SinePulse.EMS.Messages.Model.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class BranchViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public string BranchName { get; set; }
    public string BranchCode { get; set; }
    public string MobileNo { get; set; }
    public string Email { get; set; }
    public string Fax { get; set; }
    //public string MapIFrame { get; set; }
    public StatusEnum Status { get; set; }
    public InstituteViewModel Institute { get; set; } = new InstituteViewModel();
  }
}