using FluentValidation.Results;

namespace SinePulse.EMS.Messages.ClassTestMessages
{
  public class AddClassTestResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long ClassTestId { get; }

    public AddClassTestResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }

    public AddClassTestResponseMessage(ValidationResult validationResult, long classTestId)
    {
      ValidationResult = validationResult;
      ClassTestId = classTestId;
    }
  }
}