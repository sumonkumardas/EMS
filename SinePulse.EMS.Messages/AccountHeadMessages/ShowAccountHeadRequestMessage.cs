using MediatR;

namespace SinePulse.EMS.Messages.AccountHeadMessages
{
  public class ShowAccountHeadRequestMessage : IRequest<ShowAccountHeadResponseMessage>
  {
    public long AccountHeadId { get; set; }
  }
}