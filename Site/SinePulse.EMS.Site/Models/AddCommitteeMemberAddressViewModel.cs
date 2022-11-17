
namespace SinePulse.EMS.Site.Models
{
  public class AddCommitteeMemberAddressViewModel : BaseViewModel
  {
    public string PresentAddressStreet1 { get; set; }
    public string PresentAddressStreet2 { get; set; }
    public string PresentAddressPostalCode { get; set; }
    public string PresentAddressCity { get; set; }
    public string PermanentAddressStreet1 { get; set; }
    public string PermanentAddressStreet2 { get; set; }
    public string PermanentAddressPostalCode { get; set; }
    public string PermanentAddressCity { get; set; }
    public bool SameAsPermanentAddress { get; set; }
    public long CommitteeMemberId { get; set; }
  }
}
