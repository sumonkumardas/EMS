using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinePulse.EMS.UseCases.Employee
{
  public class ShowBranchMediumEmployeeListUseCase : IShowBranchMediumEmployeeListUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _autoMapperConfig;

    public ShowBranchMediumEmployeeListUseCase(IRepository repository)
    {
      _repository = repository;
      _autoMapperConfig =
        new MapperConfiguration(cfg => cfg.CreateMap<Domain.Employees.Employee, EmployeeMessageModel>());
    }

    public List<ShowEmployeeListResponseMessage.Employee> ShowBranchMediumEmployeeList(ShowBranchMediumEmployeeListRequestMessage request)
    {
      IList<Domain.Employees.Employee> dbEmployeeList = GetEmployees(request.AssociationType, request.BranchMediumId);
      var messageModelEmployeeList = new List<EmployeeMessageModel>();
      var mapper = _autoMapperConfig.CreateMapper();
      var list = mapper.Map(dbEmployeeList, messageModelEmployeeList);
      return ToRequestEmployeeList(list);
    }


    private List<ShowEmployeeListResponseMessage.Employee> ToRequestEmployeeList(
      List<EmployeeMessageModel> employeeList)
    {
      var requestEmployeeList = new List<ShowEmployeeListResponseMessage.Employee>();
      foreach (var employee in employeeList)
      {
        requestEmployeeList.Add(new ShowEmployeeListResponseMessage.Employee
        {
          Id = employee.Id,
          FullName = employee.FullName,
          EmployeeCode = employee.EmployeeCode,
          DOB = employee.DOB,
          NationalId = employee.NationalId,
          MobileNo = employee.MobileNo,
          EmailAddress = employee.EmailAddress,
          JoiningDate = employee.JoiningDate,
          BankAccountNo = employee.BankAccountNo,
          EmployeeType = employee.EmployeeType,
          Status = employee.Status,
          AssociatedWith = employee.AssociatedWith,
          ReportToId = employee.ReportTo?.Id,
          ImageUrl = employee.ImageUrl
        });
      }

      return requestEmployeeList;
    }

    private IList<Domain.Employees.Employee> GetEmployees(AssociationType associationType, long branchMediumId)
    {
      IList<Domain.Employees.Employee> dbEmployeeList;
      if (associationType == AssociationType.Global)
      {
        dbEmployeeList = _repository.Table<Domain.Employees.Employee>()
          .Where(w => w.AssociatedWith == associationType)
          .Include(nameof(Designation))
          .ToList();
      }
      else
      {
        dbEmployeeList = _repository.Table<Domain.Employees.Employee>()
          .Where(w => w.BranchMedium.Id == branchMediumId)
          .Include(nameof(Designation))
          .ToList();
      }

      return dbEmployeeList;
    }

  }
}
