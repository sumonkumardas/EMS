using System;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.ManagingCommittee;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.CommitteeMemberMessages;
using SinePulse.EMS.Messages.Model.ManagingCommittee;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.CommitteeMembers
{
  public class AddCommitteeMemberUseCase : IAddCommitteeMemberUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;
    private readonly EmsDbContext _dbContext;

    public AddCommitteeMemberUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
      _mapperConfiguration = new MapperConfiguration(c => c.CreateMap<CommitteeMember, CommitteeMemberMessageModel>());
    }

    public CommitteeMemberMessageModel AddCommitteeMember(AddCommitteeMemberRequestMessage request)
    {
      var designation = _repository.GetById<Designation>(request.DesignationId);
      var institute = _repository.GetById<Institute>(request.InstituteId);

      var committeeMember = new CommitteeMember
      {
        FullName = request.FullName,
        MobileNo = request.MobileNo,
        EmailAddress = request.EmailAddress,
        DOB = request.DOB,
        Status = request.Status,
        NationalId = request.NationalId,
        AuditFields = new AuditFields
        {
          InsertedDateTime = DateTime.Now,
          InsertedBy = request.CurrentUserName,
          LastUpdatedBy = request.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        },
        Designation = designation,
        Institute = institute
      };
      _repository.Insert(committeeMember);
      _dbContext.SaveChanges();
      var mapper = _mapperConfiguration.CreateMapper();
      return mapper.Map(committeeMember, new CommitteeMemberMessageModel());
    }
  }
}