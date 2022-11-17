using System;
using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class EmployeeSalaryPropertyChecker : IEmployeeSalaryPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public EmployeeSalaryPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsValidEffectiveDate(DateTime effectiveDate, long employeeId)
    {
      return !_dbContext.EmployeeSalaries.Any(es => es.EffectiveDate == effectiveDate && es.Employee.Id == employeeId);
    }

    public bool IsValidSalary(long? employeeGradeId, decimal sumOfSalaryComponentAmounts)
    {
      var employeeGrade = _dbContext.Grades.FirstOrDefault(g => g.Id == employeeGradeId.Value);
      if (employeeGrade != null)
      {
        return employeeGrade.MaxSalary >= sumOfSalaryComponentAmounts
               && employeeGrade.MinSalary <= sumOfSalaryComponentAmounts;
      }

      return true;
    }
  }
}