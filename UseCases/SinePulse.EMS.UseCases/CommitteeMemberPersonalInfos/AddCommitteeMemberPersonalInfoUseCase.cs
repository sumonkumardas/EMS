using SinePulse.EMS.Domain.ManagingCommittee;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.CommitteeMemberPersonalInfoMessages;
using SinePulse.EMS.Persistence.Repositories;
using System;
using System.Linq;

namespace SinePulse.EMS.UseCases.CommitteeMemberPersonalInfos
{
  public class AddCommitteeMemberPersonalInfoUseCase : IAddCommitteeMemberPersonalInfoUseCase
  {
    private readonly IRepository _repository;

    public AddCommitteeMemberPersonalInfoUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void AddCommitteeMemberPersonalInfo(AddCommitteeMemberPersonalInfoRequestMessage request)
    {
      var committeeMember = _repository.GetById<CommitteeMember>(request.CommitteeMemberId);
      var existingCommitteeMemberPersonalInfo =
        _repository.Table<CommitteeMemberPersonalInfo>()
          .FirstOrDefault(e => e.CommitteeMember.Id == request.CommitteeMemberId);

      if (existingCommitteeMemberPersonalInfo == null)
      {
        var committeeMemberPersonalInfo = new CommitteeMemberPersonalInfo
        {
          BloodGroup = request.BloodGroup,
          FatherName = request.FatherName,
          Gender = request.Gender,
          MotherName = request.MotherName,
          Religion = request.Religion,
          SpouseName = request.SpouseName,
          CommitteeMember = committeeMember,

          AuditFields = new AuditFields
          {
            InsertedBy = request.CurrentUserName,
            InsertedDateTime = DateTime.Now,
            LastUpdatedBy = request.CurrentUserName,
            LastUpdatedDateTime = DateTime.Now
          }
        };
        _repository.Insert(committeeMemberPersonalInfo);
      }
      else
      {
        existingCommitteeMemberPersonalInfo.BloodGroup = request.BloodGroup;
        existingCommitteeMemberPersonalInfo.FatherName = request.FatherName;
        existingCommitteeMemberPersonalInfo.Gender = request.Gender;
        existingCommitteeMemberPersonalInfo.MotherName = request.MotherName;
        existingCommitteeMemberPersonalInfo.Religion = request.Religion;
        existingCommitteeMemberPersonalInfo.SpouseName = request.SpouseName;
        existingCommitteeMemberPersonalInfo.AuditFields.InsertedBy = request.CurrentUserName;
        existingCommitteeMemberPersonalInfo.AuditFields.InsertedDateTime = DateTime.Now;
        existingCommitteeMemberPersonalInfo.AuditFields.LastUpdatedBy = request.CurrentUserName;
        existingCommitteeMemberPersonalInfo.AuditFields.LastUpdatedDateTime = DateTime.Now;
      }
    }
  }
}
