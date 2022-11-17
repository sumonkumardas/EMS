using System;
using SinePulse.EMS.Domain.ManagingCommittee;
using SinePulse.EMS.Messages.CommitteeMemberMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.CommitteeMembers
{
  public class AddOrChangeCommitteeMemberImageUseCase : IAddOrChangeCommitteeMemberImageUseCase
  {
    private readonly IRepository _repository;

    public AddOrChangeCommitteeMemberImageUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public string UploadCommitteeMemberImage(AddOrChangeCommitteeMemberImageRequestMessage request)
    {
      var committeeMember = _repository.GetById<CommitteeMember>(request.CommitteeMemberId);
      var previousImage = committeeMember.ImageUrl;
      committeeMember.ImageUrl = request.ImageUrl;
      committeeMember.AuditFields.LastUpdatedBy = request.CurrentUserName;
      committeeMember.AuditFields.LastUpdatedDateTime = DateTime.Now;
      if (!string.IsNullOrEmpty(request.ImageUrl))
      {
        return previousImage;
      }

      return null;
    }
  }
}