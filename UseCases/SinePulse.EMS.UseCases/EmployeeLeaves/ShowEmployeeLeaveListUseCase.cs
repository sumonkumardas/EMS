using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Leaves;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;
using SinePulse.EMS.Messages.Model.Attendance;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Messages.Model.Leaves;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.EmployeeLeaves
{
  public class ShowEmployeeLeaveListUseCase : IShowEmployeeLeaveListUseCase
  {
    private readonly IRepository _repository;
    private MapperConfiguration _automapperConfig;

    public ShowEmployeeLeaveListUseCase(IRepository repository)
    {
      _repository = repository;
      _automapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeLeave,EmployeeLeaveMessageModel>());
    }

    List<EmployeeLeaveMessageModel> IShowEmployeeLeaveListUseCase.ShowUnaprrovedLeaveList(ShowEmployeeLeaveListRequestMessage request)
    {
      var includes = new[]
      {
        nameof(EmployeeLeave.Employee),nameof(EmployeeLeave.LeaveType),
        nameof(EmployeeLeave.Employee)+"."+ nameof(EmployeeLeave.Employee.ReportTo),
        nameof(EmployeeLeave.Employee)+"."+ nameof(EmployeeLeave.Employee.BranchMedium)
      };
      List<EmployeeLeave> dbEmployeeLeaveList = new List<EmployeeLeave>();

      if (request.EmployeeId != null)
      {
        List<EmployeeLeave> dblist = _repository.Table<EmployeeLeave>(includes).ToList();
        dbEmployeeLeaveList = dblist.Where(x => x.Employee.ReportTo.Id == request.EmployeeId.Value && x.LeaveStatus == request.LeaveStatus && x.EndDate.Year == request.Year).ToList();
      }
      else if (request.BranchMediumId != null)
      {
          List<EmployeeLeave> dblist = _repository.Table<EmployeeLeave>(includes).Where(w => w.Employee.AssociatedWith == AssociationType.BranchMedium).ToList();
          dbEmployeeLeaveList = dblist.Where(x => x.Employee.BranchMedium.Id == request.BranchMediumId.Value && x.LeaveStatus == LeaveStatusEnum.Approved && x.EndDate.Year == request.Year).ToList();
      }
      else
      {
        dbEmployeeLeaveList = _repository.Table<EmployeeLeave>(includes).Where(x => x.LeaveStatus == request.LeaveStatus && x.EndDate.Year == request.Year).ToList();
      }
      var messageModelEmployeeLeaveList = new List<EmployeeLeaveMessageModel>();

      _automapperConfig =  new MapperConfiguration(cfg =>
      {
          cfg.CreateMap<Domain.Employees.Employee , EmployeeMessageModel>()
              .ForMember(x => x.ObjectId, opt => opt.Ignore())
              .ForMember(x=>x.AssociatedWith, opt=>opt.Ignore());
          cfg.CreateMap<EmployeeLeave, EmployeeLeaveMessageModel>();
          ;
      });
      var mapper = _automapperConfig.CreateMapper();
            List<EmployeeLeaveMessageModel> list = mapper.Map(dbEmployeeLeaveList, messageModelEmployeeLeaveList);
      return list;
    }
  }
}