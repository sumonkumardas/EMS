using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Taxes;
using SinePulse.EMS.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SinePulse.EMS.TaxService
{
  public class TaxCalculator : ITaxCalculator
  {
    private readonly IRepository _repository;
    public TaxCalculator(IRepository repository)
    {
      _repository = repository;
    }
    public decimal CalculateTax(int monthlySalary)
    {
      decimal remainingSalary = monthlySalary * 12;
      decimal tax = 0;
      IList<TaxRateDetail> rates = GetTaxRates();
      foreach (TaxRateDetail rate in rates)
      {
        if (rate.LimitTo != null)
        {
          if (remainingSalary > rate.LimitTo)
          {
            remainingSalary = remainingSalary - (decimal)rate.LimitTo;
            tax = tax + (decimal)rate.LimitTo * rate.Percentage/100;
          }
          else
          {
            tax = tax + remainingSalary * rate.Percentage / 100;
            break;
          }
        }
        else
        {
          tax = tax + remainingSalary * rate.Percentage / 100;
        }
      }
      return tax;
    }
    private IList<TaxRateDetail> GetTaxRates()
    {
      var taxRates = _repository.Table<TaxRate>()
                    .Include(nameof(TaxRate.TaxRateDetails))
                    .Where(w => w.EffectiveDate <= DateTime.Today)
                    .OrderByDescending(o => o.EffectiveDate)
                    .FirstOrDefault();
      if (taxRates != null) return taxRates.TaxRateDetails.OrderBy(o => o.LimitFrom).ToList();
      return new List<TaxRateDetail>();
    }
  }
}
