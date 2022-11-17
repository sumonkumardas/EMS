using FluentValidation.Results;

namespace SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages
{
  public class EditEducationalQualificationResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long EmployeeId { get; }
    
    public EditEducationalQualificationResponseMessage(ValidationResult validationResult, long employeeId)
    {
      ValidationResult = validationResult;
      EmployeeId = employeeId;
    }
  }
}