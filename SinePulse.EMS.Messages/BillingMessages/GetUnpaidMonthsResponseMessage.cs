using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.BillingMessages
{
  public class GetUnpaidMonthsResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<Month> Months { get; }

    public GetUnpaidMonthsResponseMessage(ValidationResult validationResult, List<Month> months)
    {
      ValidationResult = validationResult;
      Months = months;
    }
    
    public class Month
    {
      public string MonthName { get; set; }
      public MonthsOfYearEnum MonthEnum { get; set; }
    }
  }
}