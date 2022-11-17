using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.ContactInfoMessages
{
  public class AddContactInfoRequestMessage : IRequest<AddContactInfoResponseMessage>
  {
    public long StudentId { get; set; }
    public string PhoneNo { get; set; }
    public string EmailAddress { get; set; }
    public StatusEnum Status { get; set; }
    public string CurrentUserName { get; set; }
  }
}