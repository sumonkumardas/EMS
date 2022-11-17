using System;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.BreakTimeMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using System.Linq;

namespace SinePulse.EMS.UseCases.BreakTimes
{
  public class AddBreakTimeUseCase : IAddBreakTimeUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _autoMapperConfig;
    private readonly EmsDbContext _dbContext;

    public AddBreakTimeUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<BreakTime, BreakTimeMessageModel>());
      ;
    }

    public BreakTimeMessageModel AddBreakTime(AddBreakTimeRequestMessage message)
    {
      var mapper = _autoMapperConfig.CreateMapper();
      var breakTime = _repository.Table<BreakTime>()
        .FirstOrDefault(b => b.BranchMedium.Id == message.BranchMediumId);
      if (breakTime == null)
      {
        var branchMedium = _repository.GetById<BranchMedium>(message.BranchMediumId);
        var newBreakTime = new BreakTime
        {
          StartTime = message.StartTime,
          EndTime = message.EndTime,
          Status = message.Status,

          AuditFields = new AuditFields
          {
            InsertedBy = message.CurrentUserName,
            InsertedDateTime = DateTime.Now,
            LastUpdatedBy = message.CurrentUserName,
            LastUpdatedDateTime = DateTime.Now
          },
          BranchMedium = branchMedium
        };
        _repository.Insert(newBreakTime);
        _dbContext.SaveChanges();
        return mapper.Map(newBreakTime, new BreakTimeMessageModel());
      }

      breakTime.StartTime = message.StartTime;
      breakTime.EndTime = message.EndTime;
      breakTime.AuditFields.LastUpdatedBy = message.CurrentUserName;
      breakTime.AuditFields.LastUpdatedDateTime = DateTime.Now;
      _dbContext.SaveChanges();
      return mapper.Map(breakTime, new BreakTimeMessageModel());
    }
  }
}