using MediatR;

namespace SinePulse.EMS.Messages.CommitteeMemberMessages
{
  public class ShowCommitteeMemberRequestMessage : IRequest<ShowCommitteeMemberResponseMessage>
  {
    public long CommitteeMemberId { get; set; }
  }
}