using System;
using FluentValidation;
using SinePulse.EMS.Messages.PayrollMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Payrolls
{
  public class DefineSalaryRequestMessageValidator : AbstractValidator<DefineSalaryRequestMessage>
  {
    private readonly IEmployeeSalaryPropertyChecker _employeeSalaryPropertyChecker;
    public DefineSalaryRequestMessageValidator(IEmployeeSalaryPropertyChecker employeeSalaryPropertyChecker)
    {
      _employeeSalaryPropertyChecker = employeeSalaryPropertyChecker;
      RuleFor(x => x).Must(IsValidAmounts).WithMessage("Entered Amounts cannot be Negative!");
      RuleFor(x => x.EffectiveDate).GreaterThanOrEqualTo(DateTime.Today).WithMessage("Effective Date must be present or a future Date.");
      RuleFor(x => x.EffectiveDate).Must((m, x) => IsValidEffectiveDate(m.EffectiveDate, m.EmployeeId))
        .WithMessage("Effective Date already defined");
      RuleFor(x => x).Must((m, x) => IsValidSalary(m.EmployeeGradeId, m.TotalAmount))
        .WithMessage("Total Salary Amount must be between Minimum and Maximum Salary of Employee Grade");
    }

    private bool IsValidSalary(long? employeeGradeId, decimal totalAmount)
    {
      if (totalAmount > 0)
      {
        return _employeeSalaryPropertyChecker.IsValidSalary(employeeGradeId, totalAmount);
      }
      return true;
    }

    private bool IsValidEffectiveDate(DateTime effectiveDate, long employeeId)
    {
      return _employeeSalaryPropertyChecker.IsValidEffectiveDate(effectiveDate, employeeId);
    }

    private bool IsValidAmounts(DefineSalaryRequestMessage requestMessage)
    {
      if (requestMessage.SalaryComponentAmounts.Length > 0)
      {
        foreach (var salaryComponentAmount in requestMessage.SalaryComponentAmounts)
        {
          if (salaryComponentAmount < 0)
            return false;
        }
        return true;
      }

      return false;
    }
  }
}