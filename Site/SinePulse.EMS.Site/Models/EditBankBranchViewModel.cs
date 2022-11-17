namespace SinePulse.EMS.Site.Models
{
  public class EditBankBranchViewModel : BaseViewModel
  {
    public string BranchName { get; set; }
    public string Address { get; set; }
    public long BankBranchId { get; set; }
    public long BankId { get; set; }
  }
}