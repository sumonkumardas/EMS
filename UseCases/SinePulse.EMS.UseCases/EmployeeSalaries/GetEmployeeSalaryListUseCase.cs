using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.EmployeeSalaryMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.EmployeeSalaries
{
  public class GetEmployeeSalaryListUseCase : IGetEmployeeSalaryListUseCase
  {
    private readonly IRepository _repository;

    public GetEmployeeSalaryListUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public List<GetEmployeeSalaryListResponseMessage.EmployeeSalary> GetEmployeeSalaryList(
      GetEmployeeSalaryListRequestMessage message)
    {
      var employeeSalaries = new List<GetEmployeeSalaryListResponseMessage.EmployeeSalary>();
      var branchMediumEmployees = _repository.Table<Domain.Employees.Employee>()
        .Include(nameof(Designation))
        .Where(e => e.AssociatedWith != AssociationType.Global && 
                    e.BranchMedium != null && 
                    e.BranchMedium.Id == message.BranchMediumId)
        .ToList();
      foreach (var employee in branchMediumEmployees)
      {
        var employeeSalaryEffectiveDate = GetEmployeeSalaryEffectiveDate(employee.Id);
        var salaryComponents = GetSalaryComponents(employee.Id, employeeSalaryEffectiveDate);
        if(salaryComponents.Any())
        {
          employeeSalaries.Add(new GetEmployeeSalaryListResponseMessage.EmployeeSalary
          {
            EmployeeName = employee.FullName,
            EmployeeCode = employee.EmployeeCode,
            BankAccountNo = employee.BankAccountNo,
            Designation = employee.Designation.DesignationName,
            EmployeeId = employee.Id,
            TotalAmount = GetTotalAmount(salaryComponents),
            SalaryComponents = salaryComponents
          });
        }
      }
      return employeeSalaries;
    }

    private decimal GetTotalAmount(List<GetEmployeeSalaryListResponseMessage.SalaryComponent> salaryComponents)
    {
      if (salaryComponents != null)
      {
        var otherComponents = _repository.Table<OtherComponent>()
          .ToList();
        decimal totalAmount = 0;
        foreach (var salaryComponent in salaryComponents)
        {
          if (otherComponents.All(o => o.ComponentName != salaryComponent.ComponentName))
            totalAmount += salaryComponent.Amount;
        }

        foreach (var otherComponent in otherComponents)
        {
          var deduction = salaryComponents.FirstOrDefault(s => s.ComponentName == otherComponent.ComponentName);
          if (deduction != null)
          {
            totalAmount -= deduction.Amount;
          }
        }

        return totalAmount;
      }

      return 0;
    }

    private List<GetEmployeeSalaryListResponseMessage.SalaryComponent> GetSalaryComponents(long employeeId, 
      DateTime? employeeSalaryEffectiveDate)
    {
      var employeeSalaries = _repository.Table<EmployeeSalary>()
        .Include(nameof(PayrollComponent))
        .Where(e => e.Employee.Id == employeeId &&
                    employeeSalaryEffectiveDate != null &&
                    e.EffectiveDate == employeeSalaryEffectiveDate)
        .OrderBy(s => s.PayrollComponent.IncreaseDecrease)
        .ThenByDescending(s => s.PayrollComponent.ComponentName)
        .ToList();
      var salaryComponents = new List<GetEmployeeSalaryListResponseMessage.SalaryComponent>();
      foreach (var employeeSalary in employeeSalaries)
      {
        salaryComponents.Add(new GetEmployeeSalaryListResponseMessage.SalaryComponent
        {
          Amount = employeeSalary.Amount,
          ComponentName = employeeSalary.PayrollComponent.ComponentName
        });
      }
      return salaryComponents;
    }

    private DateTime? GetEmployeeSalaryEffectiveDate(long employeeId)
    {
      var employeeSalaryEffectiveDates = _repository.Table<EmployeeSalary>()
        .Where(es => es.Employee.Id == employeeId
                     && es.EffectiveDate <= DateTime.Today)
        .OrderByDescending(s => s.EffectiveDate)
        .Select(s => s.EffectiveDate)
        .ToList();
      if(employeeSalaryEffectiveDates.Any())
        return employeeSalaryEffectiveDates.First();
      return null;
    }
  }
}