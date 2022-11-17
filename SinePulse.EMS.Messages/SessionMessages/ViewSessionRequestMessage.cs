using MediatR;

namespace SinePulse.EMS.Messages.SessionMessages
{
  public class ViewSessionRequestMessage : IRequest<ViewSessionResponseMessage>
  {
    public long SessionId { get; set; }
    public long InstituteId { get; set; }
  }
}