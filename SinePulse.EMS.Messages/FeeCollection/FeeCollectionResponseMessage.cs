using FluentValidation.Results;

namespace SinePulse.EMS.Messages.FeeCollection
{
  public class FeeCollectionResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public FeeCollectionResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}