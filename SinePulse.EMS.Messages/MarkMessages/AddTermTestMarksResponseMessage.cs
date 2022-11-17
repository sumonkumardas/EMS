using FluentValidation.Results;

namespace SinePulse.EMS.Messages.MarkMessages
{
  public class AddTermTestMarksResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public AddTermTestMarksResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}