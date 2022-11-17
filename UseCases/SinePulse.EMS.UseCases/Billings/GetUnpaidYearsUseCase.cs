using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Billing;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.BillingMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Billings
{
  public class GetUnpaidYearsUseCase : IGetUnpaidYearsUseCase
  {
    private readonly IRepository _repository;

    public GetUnpaidYearsUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public List<int> GetUnpaidYears(GetUnpaidYearsRequestMessage message)
    {
      var payments = _repository.Table<BillingData>()
        .Where(b => b.BranchMedium.Id == message.BranchMediumId)
        .OrderByDescending(b => b.Year)
        .ThenByDescending(x => x.Month)
        .ToList();
      var today = DateTime.Today;
      var years = new List<int>();
      if (payments.Any())
      {
        if (payments.First().Month == MonthsOfYearEnum.December)
        {
          for (int i = payments.First().Year + 1; i <= today.Year; i++)
          {
            years.Add(i);
          }
        }
        else
        {
          for (int i = payments.First().Year; i <= today.Year; i++)
          {
            years.Add(i);
          }
        }

        return years.Distinct().ToList();
      }

      var branchMedium = _repository.GetById<BranchMedium>(message.BranchMediumId);

      var branchMediumCreateDate = branchMedium.AuditFields.InsertedDateTime;
      var year = branchMediumCreateDate.Year;
      if (branchMediumCreateDate.Month == 12)
        year += 1;
      for (int i = year; i <= today.Year; i++)
      {
        years.Add(i);
      }

      return years;
    }
  }
}