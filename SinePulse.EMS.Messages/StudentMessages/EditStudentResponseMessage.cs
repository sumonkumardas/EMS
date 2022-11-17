using FluentValidation.Results;

namespace SinePulse.EMS.Messages.StudentMessages
{
  public class EditStudentResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditStudentResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}