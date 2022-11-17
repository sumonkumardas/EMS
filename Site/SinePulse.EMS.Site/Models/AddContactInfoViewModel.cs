namespace SinePulse.EMS.Site.Models
{
  public class AddContactInfoViewModel: BaseViewModel
  {
    public long BranchMediumId { get; set; }
    public long StudentId { get; set; }
    public string PhoneNo { get; set; }
    public string EmailAddress { get; set; }
  }
}