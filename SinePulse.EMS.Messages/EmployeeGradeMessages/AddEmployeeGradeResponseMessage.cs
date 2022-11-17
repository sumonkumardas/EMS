using FluentValidation.Results;

namespace SinePulse.EMS.Messages.EmployeeGradeMessages
{
  public class AddEmployeeGradeResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long EmployeeGradeId { get; set; }

    public AddEmployeeGradeResponseMessage(ValidationResult validationResult,long gradeId)
    {
      ValidationResult = validationResult;
      EmployeeGradeId = gradeId;
    }
  }
}