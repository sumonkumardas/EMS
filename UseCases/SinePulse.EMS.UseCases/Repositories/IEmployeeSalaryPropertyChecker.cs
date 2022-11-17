using System;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IEmployeeSalaryPropertyChecker
  {
    bool IsValidEffectiveDate(DateTime effectiveDate, long employeeId);
    bool IsValidSalary(long? employeeGradeId, decimal sumOfSalaryComponentAmounts);
  }
}