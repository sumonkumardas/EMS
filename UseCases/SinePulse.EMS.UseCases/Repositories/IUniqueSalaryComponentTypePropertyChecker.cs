using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueSalaryComponentTypePropertyChecker
  {
    bool IsUniqueComponentType(string componentType);
    bool IsUniqueComponentType(string componentType, long salaryComponentTypId);
  }
}
