using SinePulse.EMS.Messages.BillingMessages;

namespace SinePulse.EMS.Site.Models
{
  public class BillingInfoViewModel : BaseViewModel
  {
    public int Year { get; set; }
    public GetBillingDataResponseMessage.Billing BillingData { get; set; }
  }
}