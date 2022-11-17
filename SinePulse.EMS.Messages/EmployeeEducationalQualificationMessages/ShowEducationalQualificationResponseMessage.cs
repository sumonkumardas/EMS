using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Employees;

namespace SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages
{
  public class ShowEducationalQualificationResponseMessage 
  {
    public ValidationResult ValidationResult { get; }
    public EducationalQualificationMessageModel EducationalQualification { get; }

    public ShowEducationalQualificationResponseMessage(ValidationResult validationResult, EducationalQualificationMessageModel educationalQualification)
    {
      ValidationResult = validationResult;
      EducationalQualification = educationalQualification;
    }
  }
}