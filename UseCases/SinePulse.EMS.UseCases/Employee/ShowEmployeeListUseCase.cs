using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Employee
{
  public class ShowEmployeeListUseCase : IShowEmployeeListUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _autoMapperConfig;

    public ShowEmployeeListUseCase(IRepository repository)
    {
      _repository = repository;
      _autoMapperConfig =
        new MapperConfiguration(cfg => cfg.CreateMap<Domain.Employees.Employee, EmployeeMessageModel>());
    }

    public List<ShowEmployeeListResponseMessage.Employee> ShowEmployeeList(ShowEmployeeListRequestMessage request)
    {
      long instituteId = GetInstituteId(request);
      IList<Domain.Employees.Employee> dbEmployeeList = GetEmployees(request.AssociationType, instituteId);
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

    private IList<Domain.Employees.Employee> GetEmployees(AssociationType associationType, long instituteId)
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
          .Where(w => w.Institute.Id == instituteId)
          .Include(nameof(Designation))
          .ToList();
      }

      return dbEmployeeList;
    }

    private long GetInstituteId(ShowEmployeeListRequestMessage request)
    {
      long instituteId = 0;
      switch (request.AssociationType)
      {
        case AssociationType.Institute:
          instituteId = request.ObjectId;
          return instituteId;
        case AssociationType.Branch:
          Branch branch = _repository.Filter<Branch, Institute>(
            f => f.Id == request.ObjectId,
            f => f.Institute
          ).FirstOrDefault();
          instituteId = branch.Institute.Id;
          return instituteId;
        case AssociationType.BranchMedium:
          BranchMedium branchMedium = _repository.Filter<BranchMedium, Branch, Institute>(
            f => f.Id == request.ObjectId,
            f => f.Branch,
            f => f.Branch.Institute
          ).FirstOrDefault();
          instituteId = branchMedium.Branch.Institute.Id;
          return instituteId;
        default:
          return instituteId;
      }
    }
  }
}