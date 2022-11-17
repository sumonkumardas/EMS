using SinePulse.EMS.Messages.CommitteeMemberMessages;
using SinePulse.EMS.Messages.Model.ManagingCommittee;

namespace SinePulse.EMS.UseCases.CommitteeMembers
{
  public interface IAddCommitteeMemberUseCase
  {
    CommitteeMemberMessageModel AddCommitteeMember(AddCommitteeMemberRequestMessage message);
  }
}