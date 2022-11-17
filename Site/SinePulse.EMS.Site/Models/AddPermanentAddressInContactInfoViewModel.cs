namespace SinePulse.EMS.Site.Models
{
  public class AddPermanentAddressInContactInfoViewModel : BaseViewModel
  {
    public long ContactInfoId { get; set; }
    public string Street1 { get; set; }
    public string Street2 { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public bool SameAsPresentAddress { get; set; }
  }
}