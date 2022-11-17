using SinePulse.EMS.Messages.CommitteeMemberPersonalInfoMessages;

namespace SinePulse.EMS.UseCases.CommitteeMemberPersonalInfos
{
  public interface IAddCommitteeMemberPersonalInfoUseCase
  {
    void AddCommitteeMemberPersonalInfo(AddCommitteeMemberPersonalInfoRequestMessage request);
  }
}
