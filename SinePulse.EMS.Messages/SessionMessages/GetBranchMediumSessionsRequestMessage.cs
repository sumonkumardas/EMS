using MediatR;

namespace SinePulse.EMS.Messages.SessionMessages
{
  public class GetBranchMediumSessionsRequestMessage : IRequest<GetBranchMediumSessionsResponseMessage>
  {
    public long BranchMediumId { get; set; }
  }
}