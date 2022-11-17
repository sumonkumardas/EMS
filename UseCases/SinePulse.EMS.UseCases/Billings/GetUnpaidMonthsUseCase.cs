using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Billing;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.BillingMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Billings
{
  public class GetUnpaidMonthsUseCase : IGetUnpaidMonthsUseCase
  {
    private readonly IRepository _repository;

    public GetUnpaidMonthsUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public List<GetUnpaidMonthsResponseMessage.Month> GetUnpaidMonths(GetUnpaidMonthsRequestMessage message)
    {
      var payments = _repository.Table<BillingData>()
        .Where(b => b.BranchMedium.Id == message.BranchMediumId
                    && b.Year == message.Year)
        .OrderByDescending(b => b.Year)
        .ThenByDescending(x => x.Month)
        .ToList();
      var unpaidMonths = new List<GetUnpaidMonthsResponseMessage.Month>();
      int lastPaidMonth = 0;
      if (payments.Any())
      {
        lastPaidMonth = (int) payments.First().Month;
        if (payments.First().Year == DateTime.Today.Year)
        {
          for (int i = lastPaidMonth + 1; i < DateTime.Now.Month; i++)
          {
            unpaidMonths.Add(new GetUnpaidMonthsResponseMessage.Month
            {
              MonthName = ((MonthsOfYearEnum) i).ToString("G"),
              MonthEnum = (MonthsOfYearEnum) i
            });
          }
        }
        else
        {
          for (int i = lastPaidMonth + 1; i <= (int) MonthsOfYearEnum.December; i++)
          {
            unpaidMonths.Add(new GetUnpaidMonthsResponseMessage.Month
            {
              MonthName = ((MonthsOfYearEnum) i).ToString("G"),
              MonthEnum = (MonthsOfYearEnum) i
            });
          }
        }
      }
      else
      {        
        var allpayments = _repository.Table<BillingData>()
          .Where(b => b.BranchMedium.Id == message.BranchMediumId)
          .OrderByDescending(b => b.Year)
          .ThenByDescending(x => x.Month)
          .ToList();
        if (allpayments.Any())
        {
          for (int i = lastPaidMonth + 1; i < DateTime.Now.Month; i++)
          {
            unpaidMonths.Add(new GetUnpaidMonthsResponseMessage.Month
            {
              MonthName = ((MonthsOfYearEnum) i).ToString("G"),
              MonthEnum = (MonthsOfYearEnum) i
            });
          }
        }
        else
        {
          var branchMedium = _repository.GetById<BranchMedium>(message.BranchMediumId);

          var branchMediumCreateDate = branchMedium.AuditFields.InsertedDateTime;
          for (int i = branchMediumCreateDate.Month; i < DateTime.Now.Month; i++)
          {
            unpaidMonths.Add(new GetUnpaidMonthsResponseMessage.Month
            {
              MonthName = ((MonthsOfYearEnum) i).ToString("G"),
              MonthEnum = (MonthsOfYearEnum) i
            });
          }
        }
       
      }

      return unpaidMonths;
    }
  }
}