using System.Collections.Generic;
using FluentValidation.Results;

namespace SinePulse.EMS.Messages.BillingMessages
{
  public class GetUnpaidYearsResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<int> Years { get; }

    public GetUnpaidYearsResponseMessage(ValidationResult validationResult, List<int> years)
    {
      ValidationResult = validationResult;
      Years = years;
    }
  }
}