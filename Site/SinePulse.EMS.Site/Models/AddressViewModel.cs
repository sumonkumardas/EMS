namespace SinePulse.EMS.Site.Models
{
  public class AddressViewModel : BaseViewModel
  {
    public string PresentStreet1 { get; set; }
    public string PresentStreet2 { get; set; }
    public string PresentPostalCode { get; set; }
    public string PresentCity { get; set; }
    
    public string PermanentStreet1 { get; set; }
    public string PermanentStreet2 { get; set; }
    public string PermanentPostalCode { get; set; }
    public string PermanentCity { get; set; }
  }
}