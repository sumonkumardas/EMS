using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.TaxService
{
  public interface ITaxCalculator
  {
    decimal CalculateTax(int monthlySalary);
  }
}
