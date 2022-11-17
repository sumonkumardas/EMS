using MediatR;
using SinePulse.EMS.Messages.Model.Enums;

namespace SinePulse.EMS.Messages.ClassMessages
{
  public class EditClassRequestMessage : IRequest<EditClassResponseMessage>
  {
    public long ClassId { get; set; }
    public string ClassName { get; set; }
    public int ClassCode { get; set; }
    public StatusEnum Status { get; set; }
    public string CurrentUserName { get; set; }
  }
}