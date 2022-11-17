using MediatR;

namespace SinePulse.EMS.Messages.SessionMessages
{
  public class ShowCurrentSessionListRequestMessage : IRequest<ShowCurrentSessionListResponseMessage>
  {
    public long BranchMediumId { get; set; }
  }
}
