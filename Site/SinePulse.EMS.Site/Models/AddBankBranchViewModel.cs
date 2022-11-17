namespace SinePulse.EMS.Site.Models
{
  public class AddBankBranchViewModel : BaseViewModel
  {
    public string BranchName { get; set; }
    public string Address { get; set; }
    public long BankId { get; set; }
  }
}