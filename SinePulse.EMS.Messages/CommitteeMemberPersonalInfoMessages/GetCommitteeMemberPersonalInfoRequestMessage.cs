using MediatR;

namespace SinePulse.EMS.Messages.CommitteeMemberPersonalInfoMessages
{
  public class GetCommitteeMemberPersonalInfoRequestMessage : IRequest<GetCommitteeMemberPersonalInfoResponseMessage>
  {
    public long CommitteeMemberId { get; set; }
  }
}
