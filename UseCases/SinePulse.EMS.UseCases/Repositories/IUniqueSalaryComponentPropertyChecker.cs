using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueSalaryComponentPropertyChecker
  {
    bool IsUniqueComponentName(string componentName);
    bool IsUniqueComponentName(string componentName, long salaryComponentId);
  }
}
