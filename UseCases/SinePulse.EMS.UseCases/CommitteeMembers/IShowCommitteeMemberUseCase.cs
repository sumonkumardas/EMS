using SinePulse.EMS.Messages.CommitteeMemberMessages;
using SinePulse.EMS.Messages.Model.ManagingCommittee;

namespace SinePulse.EMS.UseCases.CommitteeMembers
{
  public interface IShowCommitteeMemberUseCase
  {
    CommitteeMemberMessageModel GetCommitteeMember(ShowCommitteeMemberRequestMessage message);
  }
}