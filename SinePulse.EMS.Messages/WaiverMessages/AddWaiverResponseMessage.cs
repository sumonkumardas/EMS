using FluentValidation.Results;

namespace SinePulse.EMS.Messages.WaiverMessages
{
  public class AddWaiverResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public AddWaiverResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}