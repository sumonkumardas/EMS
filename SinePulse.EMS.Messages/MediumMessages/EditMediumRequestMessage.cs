using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.MediumMessages
{
  public class EditMediumRequestMessage : IRequest<EditMediumResponseMessage>
  {
    public long MediumId { get; set; }
    public string MediumName { get; set; }
    public string MediumCode { get; set; }
    public StatusEnum Status { get; set; }
    public string CurrentUserName{ get; set; }
  }
}