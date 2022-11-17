using MediatR;

namespace SinePulse.EMS.Messages.BreakTimeMessages
{
  public class GetClassBreakTimeRequestMessage : IRequest<GetClassBreakTimeResponseMessage>
  {
    public long BranchMediumId { get; set; }
  }
}