using MediatR;

namespace SinePulse.EMS.Messages.CommitteeMemberMessages
{
  public class AddOrChangeCommitteeMemberImageRequestMessage : IRequest<AddOrChangeCommitteeMemberImageResponseMessage>
  {
    public long CommitteeMemberId { get; set; }
    public string ImageUrl { get; set; }
    public string CurrentUserName { get; set; }
  }
}