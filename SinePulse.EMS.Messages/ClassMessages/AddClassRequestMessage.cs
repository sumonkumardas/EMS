using MediatR;

namespace SinePulse.EMS.Messages.ClassMessages
{
  public class AddClassRequestMessage : IRequest<AddClassResponseMessage>
  {
    public string ClassName { get; set; }
    public int ClassCode { get; set; }
    public string CurrentUserName { get; set; }
  }
}