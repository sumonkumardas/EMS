using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.ClassMessages
{
  public class ShowClassRequestMessage : IRequest<ShowClassResponseMessage>
  {
    public long ClassId { get; set; }
    public string ClassName { get; set; }
    public string ClassCode { get; set; }
    public StatusEnum Status { get; set; }
    public string CurrentUserName { get; set; }
  }
}