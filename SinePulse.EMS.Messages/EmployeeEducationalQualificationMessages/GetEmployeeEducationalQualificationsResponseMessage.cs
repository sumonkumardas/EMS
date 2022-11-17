using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Employees;

namespace SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages
{
  public class GetEmployeeEducationalQualificationsResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<EducationalQualificationMessageModel> EducationalQualifications { get; }

    public GetEmployeeEducationalQualificationsResponseMessage(ValidationResult validationResult,
      IEnumerable<EducationalQualificationMessageModel> educationalQualifications)
    {
      ValidationResult = validationResult;
      EducationalQualifications = educationalQualifications;
    }
  }
}