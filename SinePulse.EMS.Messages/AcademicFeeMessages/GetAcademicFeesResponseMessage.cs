using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.Messages.AcademicFeeMessages
{
  public class GetAcademicFeesResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<AcademicFeeMessageModel> AcademicFees { get; }
    public GetAcademicFeesResponseMessage(ValidationResult validationResult, IEnumerable<AcademicFeeMessageModel> academicFees)
    {
      ValidationResult = validationResult;
      AcademicFees = academicFees;
    }
  }
}