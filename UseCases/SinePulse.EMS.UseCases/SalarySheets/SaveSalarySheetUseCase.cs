using System;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.SalarySheetMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.SalarySheets
{
  public class SaveSalarySheetUseCase : ISaveSalarySheetUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public SaveSalarySheetUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void SaveSalarySheet(SaveSalarySheetRequestMessage message)
    {
      var salarySheet = _repository.Table<SalarySheet>()
        .FirstOrDefault(s => s.Year == message.Year &&
                             s.Month == message.Month &&
                             s.BranchMedium.Id == message.BranchMediumId);
      if (salarySheet != null)
      {
        salarySheet.TotalOtherDeduction = message.OtherDeductionAmounts.Sum();
        salarySheet.TotalSalary = message.TotalAmount;
        var indexOfOtherComponent = -1;
        foreach (var employeeId in message.EmployeeIds)
        {
          ++indexOfOtherComponent;
          var salarySheetDetails = _repository.Table<SalarySheetDetail>()
            .FirstOrDefault(s => s.Employee.Id == employeeId &&
                                 s.SalarySheet.Id == salarySheet.Id &&
                                 s.PayrollComponent.ComponentName == "Other Deduction");
          if (salarySheetDetails != null)
            salarySheetDetails.Amount = message.OtherDeductionAmounts[indexOfOtherComponent];
        }
        _dbContext.SaveChanges();
        return;
      }
      var branchMedium = _repository.GetById<BranchMedium>(message.BranchMediumId);
      var newSalarySheet = new SalarySheet
      {
        AuditFields = new AuditFields
        {
          InsertedBy = message.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = message.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        },
        BranchMedium = branchMedium,
        GenerationDate = DateTime.Now,
        IsAccountPosted = false,
        Year = message.Year,
        Month = message.Month,
        TotalOtherDeduction = message.OtherDeductionAmounts.Sum(),
        TotalSalary = message.TotalAmount,
        TotalTaxes = message.TotalTaxDeduction
      };
      _repository.Insert(newSalarySheet);
      var index = -1;
      var salaryComponentsIndex = -1;
      foreach (var employeeId in message.EmployeeIds)
      {
        index++;
        var employee = _repository.GetById<Domain.Employees.Employee>(employeeId);
        foreach (var componentName in message.SalaryComponentNames)
        {
          salaryComponentsIndex++;
          var salaryComponent = _repository.Table<PayrollComponent>()
            .FirstOrDefault(s => s.ComponentName == componentName);
          if (salaryComponent != null && salaryComponent.ComponentName == "Other Deduction")
          {
            var salarySheetDetails = new SalarySheetDetail
            {
              Employee = employee,
              SalarySheet = newSalarySheet,
              Amount = message.OtherDeductionAmounts[index],
              PayrollComponent = salaryComponent,
              BankAccountNo = message.EmployeeBankAccounts[index]
            };
            _repository.Insert(salarySheetDetails);
          }
          else
          {
            var salarySheetDetails = new SalarySheetDetail
            {
              Employee = employee,
              SalarySheet = newSalarySheet,
              Amount = message.SalaryComponentAmounts[salaryComponentsIndex],
              PayrollComponent = salaryComponent,
              BankAccountNo = message.EmployeeBankAccounts[index]
            };
            _repository.Insert(salarySheetDetails);
          }
        }
      }

      _dbContext.SaveChanges();
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