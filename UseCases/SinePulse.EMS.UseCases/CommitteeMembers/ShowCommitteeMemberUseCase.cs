using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.ManagingCommittee;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.CommitteeMemberMessages;
using SinePulse.EMS.Messages.Model.ManagingCommittee;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.CommitteeMembers
{
  public class ShowCommitteeMemberUseCase : IShowCommitteeMemberUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public ShowCommitteeMemberUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(c => c.CreateMap<CommitteeMember, CommitteeMemberMessageModel>());
    }

    public CommitteeMemberMessageModel GetCommitteeMember(ShowCommitteeMemberRequestMessage message)
    {
      var committeeMember = _repository.Table<CommitteeMember>()
        .Include(nameof(Institute))
        .Include(nameof(Designation))
        .Include(nameof(CommitteeMember.PermanentAddress))
        .Include(nameof(CommitteeMember.PresentAddress))
        .FirstOrDefault(c => c.Id == message.CommitteeMemberId);
      var mapper = _mapperConfiguration.CreateMapper();
      return mapper.Map(committeeMember, new CommitteeMemberMessageModel());
    }
  }
}