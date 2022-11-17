using FluentValidation.Results;

namespace SinePulse.EMS.Messages.StudentSectionMessages
{
  public class AddStudentSectionResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public AddStudentSectionResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}