using FluentValidation.Results;

namespace SinePulse.EMS.Messages.ClassMessages
{
  public class AddClassResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long ClassId { get; }

    public AddClassResponseMessage(ValidationResult validationResult, long classId)
    {
      ValidationResult = validationResult;
      ClassId = classId;
    }

    public AddClassResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}