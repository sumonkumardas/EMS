using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Students
{
  public class ContactInfoMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties

    public string PhoneNo { get; set; }
    public string EmailAddress { get; set; }
    public StatusEnum Status { get; set; }

    #endregion

    #region  Navigation Properties

    public AddressMessageModel PresentAddress { get; set; }
    public AddressMessageModel PermanentAddress { get; set; }

    #endregion
  }
}