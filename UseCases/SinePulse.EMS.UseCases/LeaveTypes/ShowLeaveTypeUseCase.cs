using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Domain.Leaves;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.LeaveTypeMessages;
using SinePulse.EMS.Messages.Model.Leaves;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.LeaveTypes
{
  public class ShowLeaveTypeUseCase : IShowLeaveTypeUseCase
  {
    private readonly IRepository _repository;
    private MapperConfiguration _automapperConfig;

    public ShowLeaveTypeUseCase(IRepository repository)
    {
      _repository = repository;
      _automapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<LeaveType, LeaveTypeMessageModel>());
    }

    public LeaveTypeMessageModel ShowLeaveType(ShowLeaveTypeRequestMessage message)
    {

      //var includes = new[]
      // {
      //  nameof(Branch),nameof(Medium)
      //};
      var dbLeaveType = _repository.GetById<LeaveType>(message.LeaveTypeId);
      var messageModelLeaveType = new LeaveTypeMessageModel();

      var mapper = _automapperConfig.CreateMapper();
      LeaveTypeMessageModel model = mapper.Map(dbLeaveType, messageModelLeaveType);
      return model;
    }
  }
}