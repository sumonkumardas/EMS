using SinePulse.EMS.Domain.ManagingCommittee;
using SinePulse.EMS.Messages.CommitteeMemberPersonalInfoMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility;
using System.Linq;

namespace SinePulse.EMS.UseCases.CommitteeMemberPersonalInfos
{
  public class GetCommitteeMemberPersonalInfoUseCase : IGetCommitteeMemberPersonalInfoUseCase
  {
    private readonly IRepository _repository;

    public GetCommitteeMemberPersonalInfoUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public GetCommitteeMemberPersonalInfoResponseMessage.CommitteeMemberPersonalInfo GetCommitteeMemberPersonalInfo(
      GetCommitteeMemberPersonalInfoRequestMessage message)
    {
      var personalInfoResponse = new GetCommitteeMemberPersonalInfoResponseMessage.CommitteeMemberPersonalInfo();
      var personalInfo = _repository.Table<CommitteeMemberPersonalInfo>()
        .FirstOrDefault(e => e.CommitteeMember.Id == message.CommitteeMemberId);
      if(personalInfo != null)
      {
        personalInfoResponse.BloodGroup = personalInfo.BloodGroup.GetEnumDisplayName();
        personalInfoResponse.FathersName = personalInfo.FatherName;
        personalInfoResponse.MothersName = personalInfo.MotherName;
        personalInfoResponse.SpouseName = personalInfo.SpouseName;
        personalInfoResponse.Religion = personalInfo.Religion.ToString("G");
        personalInfoResponse.ReligionEnum = personalInfo.Religion;
        personalInfoResponse.Gender = personalInfo.Gender.ToString("G");
        personalInfoResponse.GenderEnum = personalInfo.Gender;
        personalInfoResponse.BloodGroupEnum = personalInfo.BloodGroup;
      }
      

      return personalInfoResponse;
    }


  }
}
