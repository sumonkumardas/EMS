using SinePulse.EMS.Messages.CommitteeMemberMessages;

namespace SinePulse.EMS.UseCases.CommitteeMembers
{
  public interface IAddOrChangeCommitteeMemberImageUseCase
  {
    string UploadCommitteeMemberImage(AddOrChangeCommitteeMemberImageRequestMessage request);
  }
}