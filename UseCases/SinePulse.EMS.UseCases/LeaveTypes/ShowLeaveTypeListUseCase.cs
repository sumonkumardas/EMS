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
  public class ShowLeaveTypeListUseCase : IShowLeaveTypeListUseCase
  {
    private readonly IRepository _repository;
    private MapperConfiguration _automapperConfig;

    public ShowLeaveTypeListUseCase(IRepository repository)
    {
      _repository = repository;
      _automapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<LeaveType, LeaveTypeMessageModel>());
    }

    public List<LeaveTypeMessageModel> ShowLeaveTypeList(ShowLeaveTypeListRequestMessage message)
    {
      var dbLeaveTypeList = _repository.Table<LeaveType>().ToList();
      var messageModelLeaveTypeList = new List<LeaveTypeMessageModel>();

      var mapper = _automapperConfig.CreateMapper();
      List<LeaveTypeMessageModel> list = mapper.Map(dbLeaveTypeList, messageModelLeaveTypeList);
      return list;
    }
  }
}