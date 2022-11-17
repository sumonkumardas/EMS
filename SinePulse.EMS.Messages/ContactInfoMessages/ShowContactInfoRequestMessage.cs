using MediatR;

namespace SinePulse.EMS.Messages.ContactInfoMessages
{
  public class ShowContactInfoRequestMessage : IRequest<ShowContactInfoResponseMessage>
  {
    public long ContactInfoId { get; set; }
  }
}