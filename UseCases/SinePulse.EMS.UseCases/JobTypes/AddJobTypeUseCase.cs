using System;
using AutoMapper;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.JobTypeMessages;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.JobTypes
{
  public class AddJobTypeUseCase : IAddJobTypeUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _autoMapperConfig;
    private readonly EmsDbContext _dbContext;

    public AddJobTypeUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<JobType, JobTypeMessageModel>());
    }

    public JobTypeMessageModel AddJobType(AddJobTypeRequestMessage requestMessage)
    {
      var jobType = new JobType
      {
        JobTitle = requestMessage.Title,
        HasOverTime = requestMessage.HasOverTime,
        Status = requestMessage.Status,

        AuditFields = new AuditFields
        {
          InsertedBy = requestMessage.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = requestMessage.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };

      _repository.Insert(jobType);
      _dbContext.SaveChanges();
      var mapper = _autoMapperConfig.CreateMapper();
      return mapper.Map(jobType, new JobTypeMessageModel());
    }
  }
}