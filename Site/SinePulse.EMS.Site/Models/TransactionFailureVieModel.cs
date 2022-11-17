namespace SinePulse.EMS.Site.Models
{
  public class TransactionFailureVieModel : BaseViewModel
  {
    public long BranchMediumId { get; set; }
    public string TransactionId { get; set; }
    public decimal TransactionAmount { get; set; }
    
  }
}