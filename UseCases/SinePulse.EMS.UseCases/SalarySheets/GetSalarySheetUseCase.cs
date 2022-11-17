using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.EmployeeSalaryMessages;
using SinePulse.EMS.Messages.SalarySheetMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.SalarySheets
{
  public class GetSalarySheetUseCase : IGetSalarySheetUseCase
  {
    private readonly IRepository _repository;

    public GetSalarySheetUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public List<GetSalarySheetResponseMessage.EmployeeSalary> GetSalarySheet(GetSalarySheetRequestMessage message)
    {
      var salarySheet = _repository.Table<SalarySheet>()
        .FirstOrDefault(s => s.BranchMedium.Id == message.BranchMediumId &&
                             s.Year == message.Year &&
                             s.Month == message.Month);
      var requestSalarySheet = new List<GetSalarySheetResponseMessage.EmployeeSalary>();
      if (salarySheet != null)
      {
        var employeeSalaryDetails = _repository.Table<SalarySheetDetail>()
          .Include(nameof(Domain.Employees.Employee))
          .Include(nameof(Domain.Employees.Employee) + "." + nameof(Designation))
          .Include(nameof(PayrollComponent))
          .Where(s => s.SalarySheet.Id == salarySheet.Id)
          .GroupBy(s => s.Employee.Id)
          .ToList();

        foreach (var salarySheetDetails in employeeSalaryDetails)
        {
          var employeeSalary = new GetSalarySheetResponseMessage.EmployeeSalary();
          var salaryComponents = salarySheetDetails
            .OrderBy(s => s.PayrollComponent.IncreaseDecrease)
            .ThenByDescending(s => s.PayrollComponent.ComponentName)
            .ToList();
          employeeSalary.EmployeeId = salaryComponents[0].Employee.Id;
          employeeSalary.EmployeeName = salaryComponents[0].Employee.FullName;
          employeeSalary.EmployeeCode = salaryComponents[0].Employee.EmployeeCode;
          employeeSalary.Designation = salaryComponents[0].Employee.Designation.DesignationName;
          employeeSalary.BankAccountNo = salaryComponents[0].Employee.BankAccountNo;
          employeeSalary.TotalAmount = GetTotalAmount(salaryComponents);
          var requestSalaryComponents = new List<GetSalarySheetResponseMessage.SalaryComponent>();
          foreach (var salaryComponent in salaryComponents)
          {
            requestSalaryComponents.Add(new GetSalarySheetResponseMessage.SalaryComponent
            {
              Amount = salaryComponent.Amount,
              ComponentName = salaryComponent.PayrollComponent.ComponentName,
              ComponentType = salaryComponent.PayrollComponent.IncreaseDecrease
            });
          }

          employeeSalary.SalaryComponents = requestSalaryComponents;
          requestSalarySheet.Add(employeeSalary);
        }
      }

      return requestSalarySheet;
    }

    private decimal GetTotalAmount(List<SalarySheetDetail> salaryComponents)
    {
      if (salaryComponents != null)
      {
        var otherComponents = _repository.Table<OtherComponent>()
          .ToList();
        decimal totalAmount = 0;
        foreach (var salaryComponent in salaryComponents)
        {
          if (otherComponents.All(o => o.ComponentName != salaryComponent.PayrollComponent.ComponentName))
            totalAmount += salaryComponent.Amount;
        }

        foreach (var otherComponent in otherComponents)
        {
          var deduction =
            salaryComponents.FirstOrDefault(s => s.PayrollComponent.ComponentName == otherComponent.ComponentName);
          if (deduction != null)
          {
            totalAmount -= deduction.Amount;
          }
        }

        return totalAmount;
      }

      return 0;
    }
  }
}