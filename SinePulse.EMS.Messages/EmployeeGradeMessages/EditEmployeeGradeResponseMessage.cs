using FluentValidation.Results;

namespace SinePulse.EMS.Messages.EmployeeGradeMessages
{
  public class EditEmployeeGradeResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditEmployeeGradeResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}