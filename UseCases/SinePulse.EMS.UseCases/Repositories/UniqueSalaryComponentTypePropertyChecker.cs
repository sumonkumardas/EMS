using SinePulse.EMS.Persistence.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueSalaryComponentTypePropertyChecker : IUniqueSalaryComponentTypePropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueSalaryComponentTypePropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }
    public bool IsUniqueComponentType(string componentType)
    {
      return !_dbContext.SalaryComponentTypes.Any(ct => ct.ComponentType == componentType);
    }

    public bool IsUniqueComponentType(string componentType, long salaryComponentTypId)
    {
      return !_dbContext.SalaryComponentTypes.Any(ct => ct.ComponentType == componentType && ct.Id != salaryComponentTypId);
    }
  }
}
