using SinePulse.EMS.Persistence.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueSalaryComponentPropertyChecker : IUniqueSalaryComponentPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueSalaryComponentPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }
    
    public bool IsUniqueComponentName(string componentName)
    {
      return !_dbContext.SalaryComponents.Any(ct => ct.ComponentName == componentName);
    }

    public bool IsUniqueComponentName(string componentName, long salaryComponentId)
    {
      return !_dbContext.SalaryComponents.Any(ct => ct.ComponentName == componentName && ct.Id != salaryComponentId);
    }
  }
}
