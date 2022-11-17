using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Leaves;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;
using SinePulse.EMS.Messages.Model.Leaves;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.EmployeeLeaves
{
  public class GetEmployeeLeavesUseCase : IGetEmployeeLeavesUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public GetEmployeeLeavesUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(c => c.CreateMap<EmployeeLeave, EmployeeLeaveMessageModel>());
    }

    public List<EmployeeLeaveMessageModel> GetEmployeeLeaves(GetEmployeeLeavesRequestMessage message)
    {
      var mapper = _mapperConfiguration.CreateMapper();
      if (message.EndDate != null && message.StartDate != null)
      {
        var filteredEmployeeLeaves = _repository.TableNoTracking<EmployeeLeave>()
          .Include(nameof(LeaveType))
          .Where(el => el.Employee.Id == message.EmployeeId
                       && el.StartDate >= message.StartDate
                       && el.StartDate <= message.EndDate)
          .ToList();
        return mapper.Map(filteredEmployeeLeaves, new List<EmployeeLeaveMessageModel>());
      }
      var employeeLeaves = _repository.TableNoTracking<EmployeeLeave>()
        .Include(nameof(LeaveType))
        .Where(el => el.Employee.Id == message.EmployeeId
                     && el.StartDate.Year == DateTime.Now.Year)
        .ToList();
      return mapper.Map(employeeLeaves, new List<EmployeeLeaveMessageModel>());
    }
  }
}