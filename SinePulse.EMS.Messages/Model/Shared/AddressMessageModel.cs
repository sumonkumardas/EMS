

namespace SinePulse.EMS.Messages.Model.Shared
{
  public class AddressMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties
    public string Street1 { get; set; }
    public string Street2 { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    #endregion
  }
}
