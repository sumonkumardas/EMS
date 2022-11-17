using System;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Sessions
{
  public class AddSessionUseCase : IAddSessionUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;
    private readonly MapperConfiguration _autoMapperConfig;

    public AddSessionUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Session, SessionMessageModel>());
    }

    public SessionMessageModel AddSession(AddSessionRequestMessage request)
    {
      var institute = request.SessionType == ObjectTypeEnum.Institute
        ? _repository.GetById<Institute>(request.ObjectId)
        : null;
      var branch = request.SessionType == ObjectTypeEnum.Branch
        ? _repository.GetById<Branch>(request.ObjectId)
        : null;
      var branchMedium = (request.SessionType == ObjectTypeEnum.Medium ||
                          request.SessionType == ObjectTypeEnum.BranchMedium)
        ? _repository.GetById<BranchMedium>(request.ObjectId)
        : null;

      var session = new Session
      {
        SessionName = request.SessionName,
        StartDate = request.StartDate,
        EndDate = request.EndDate,
        Status = StatusEnum.Active,
        IsSessionClosed = false,
        SessionType = request.SessionType,

        AuditFields = new AuditFields
        {
          InsertedBy = request.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = request.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        },

        Institute = institute,
        Branch = branch,
        BranchMedium = branchMedium
      };

      _repository.Insert(session);
      _dbContext.SaveChanges();
      var mapper = _autoMapperConfig.CreateMapper();
      return mapper.Map(session, new SessionMessageModel());
    }
  }
}