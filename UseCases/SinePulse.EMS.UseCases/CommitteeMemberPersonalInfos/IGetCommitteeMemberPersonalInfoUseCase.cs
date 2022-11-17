using SinePulse.EMS.Messages.CommitteeMemberPersonalInfoMessages;

namespace SinePulse.EMS.UseCases.CommitteeMemberPersonalInfos
{
  public interface IGetCommitteeMemberPersonalInfoUseCase
  {
    GetCommitteeMemberPersonalInfoResponseMessage.CommitteeMemberPersonalInfo GetCommitteeMemberPersonalInfo(
      GetCommitteeMemberPersonalInfoRequestMessage message);
  }
}
