using FluentValidation.Results;

namespace SinePulse.EMS.Messages.StudentMessages
{
  public class AddStudentAddressResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public AddStudentAddressResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}