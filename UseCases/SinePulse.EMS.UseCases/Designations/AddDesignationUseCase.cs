using System;
using AutoMapper;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.DesignationMessages;
using SinePulse.EMS.Messages.Model.Shared;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Designations
{
  public class AddDesignationUseCase : IAddDesignationUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _autoMapperConfig;
    private readonly EmsDbContext _dbContext;

    public AddDesignationUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Designation, DesignationMessageModel>());
    }

    public DesignationMessageModel AddDesignation(AddDesignationRequestMessage requestMessage)
    {
      var designation = new Designation
      {
        DesignationName = requestMessage.DesignationName,
        Status = requestMessage.Status,
        EmployeeType = requestMessage.EmployeeType,

        AuditFields = new AuditFields
        {
          InsertedBy = requestMessage.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = requestMessage.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };
      _repository.Insert(designation);
      _dbContext.SaveChanges();
      var mapper = _autoMapperConfig.CreateMapper();
      return mapper.Map(designation, new DesignationMessageModel());
    }
  }
}