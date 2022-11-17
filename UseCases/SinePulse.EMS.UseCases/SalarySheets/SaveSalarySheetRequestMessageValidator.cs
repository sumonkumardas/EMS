using System.Collections.Generic;
using FluentValidation;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.GenerateSalarySheetMessages;
using SinePulse.EMS.Messages.SalarySheetMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.SalarySheets
{
  public class SaveSalarySheetRequestMessageValidator : AbstractValidator<SaveSalarySheetRequestMessage>
  {
    private readonly ISaveSalarySheetValidationChecker _saveSalarySheetValidationChecker;
    public SaveSalarySheetRequestMessageValidator(ISaveSalarySheetValidationChecker saveSalarySheetValidationChecker)
    {
      _saveSalarySheetValidationChecker = saveSalarySheetValidationChecker;
      RuleFor(x => x.Month).GreaterThan(0).WithMessage("Select Month");
      RuleFor(x => x.Year).GreaterThan(0).WithMessage("Select Year");
      RuleFor(x => x).Must((m, x) => IsValidEmployeeBankAccounts(m.EmployeeBankAccounts))
        .WithMessage("All Employees must have a Bank Account.");
      RuleFor(x => x).Must((m, x) => IsEmployeesExists(m.EmployeeIds)).WithMessage("Salary not defined. At first define Employee Salary.");
//      RuleFor(x => x).Must((m, x) => IsSalarySheetAlreadySaved(m.Year, m.Month, m.BranchMediumId))
//        .WithMessage(x => $"Salary sheet for {(MonthsOfYearEnum)x.Month:G} already saved.");
    }

    private bool IsSalarySheetAlreadySaved(int year, int month, long branchMediumId)
    {
      return _saveSalarySheetValidationChecker.IsSalarySheetAlreadySaved(year, month, branchMediumId);
    }

    private bool IsEmployeesExists(List<long> employeeIds)
    {
      if (employeeIds == null || employeeIds.Count <= 0)
        return false;
      return true;
    }

    private bool IsValidEmployeeBankAccounts(List<string> employeeBankAccounts)
    {
      if (employeeBankAccounts != null)
      {
        foreach (var employeeBankAccount in employeeBankAccounts)
        {
          if (employeeBankAccount == null)
          {
            return false;
          }
        }
      }

      return true;
    }
  }
}