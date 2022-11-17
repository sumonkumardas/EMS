using System;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.ShiftMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Shifts
{
  public class AddShiftUseCase : IAddShiftUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;
    private readonly MapperConfiguration _autoMapperConfig;

    public AddShiftUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Shift, ShiftMessageModel>());
    }

    public ShiftMessageModel AddShift(AddShiftRequestMessage request)
    {
      var branch = _repository.GetById<Branch>(request.BranchId);

      var shift = new Shift
      {
        ShiftName = request.ShiftName,
        StartTime = request.StartTime,
        EndTime = request.EndTime,
        Status = StatusEnum.Active,

        AuditFields = new AuditFields
        {
          InsertedBy = request.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = request.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        },

        Branch = branch
      };

      _repository.Insert(shift);
      _dbContext.SaveChanges();
      var mapper = _autoMapperConfig.CreateMapper();
      return mapper.Map(shift, new ShiftMessageModel());
    }
  }
}