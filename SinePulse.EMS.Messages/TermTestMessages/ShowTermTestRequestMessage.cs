using MediatR;

namespace SinePulse.EMS.Messages.TermTestMessages
{
  public class ShowTermTestRequestMessage : IRequest<ShowTermTestResponseMessage>
  {
    public long TermTestId { get; set; }
  }
}