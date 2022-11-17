namespace SinePulse.EMS.Site.Models
{
  public class TransactionSuccessViewModel : BaseViewModel
  {
    public long BranchMediumId { get; set; }
    public string TransactionId { get; set; }
    public decimal TransactionAmount { get; set; }
    public string PaymentFor { get; set; }
  }
}