using System.Collections.Generic;
using FluentValidation.Results;

namespace SinePulse.EMS.Messages.GenerateSalarySheetMessages
{
  public class GetSalarySheetYearsResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<int> Years { get; }

    public GetSalarySheetYearsResponseMessage(ValidationResult validationResult, List<int> years)
    {
      ValidationResult = validationResult;
      Years = years;
    }
  }
}