using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Site.Models
{
  public class ContactInfoViewModel : BaseViewModel
  {
    public long ContactInfoId { get; set; }
    public string PhoneNo { get; set; }
    public string EmailAddress { get; set; }
    public StatusEnum Status { get; set; }
    public AddressMessageModel PresentAddress { get; set; }
    public AddressMessageModel PermanentAddress { get; set; }
  }
}