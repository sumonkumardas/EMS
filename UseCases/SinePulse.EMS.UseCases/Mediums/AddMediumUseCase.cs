using System;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.MediumMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Mediums
{
  public class AddMediumUseCase : IAddMediumUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;
    private readonly MapperConfiguration _autoMapperConfig;

    public AddMediumUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Medium, MediumMessageModel>());
    }

    public MediumMessageModel AddMedium(AddMediumRequestMessage request)
    {
      var medium = new Medium
      {
        MediumName = request.MediumName,
        MediumCode = request.MediumCode,
        Status = StatusEnum.Active,

        AuditFields = new AuditFields
        {
          InsertedBy = request.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = request.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };

      _repository.Insert(medium);
      _dbContext.SaveChanges();
      var mapper = _autoMapperConfig.CreateMapper();
      return mapper.Map(medium, new MediumMessageModel());
    }
  }
}