using MediatR;

namespace SinePulse.EMS.Messages.ContactInfoMessages
{
  public class AddPresentAddressInContactInfoRequestMessage : IRequest<AddPresentAddressInContactInfoResponseMessage>
  {
    public long ContactInfoId { get; set; }
    public string Street1 { get; set; }
    public string Street2 { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string CurrentUserName { get; set; }
  }
}