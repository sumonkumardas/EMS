using FluentValidation.Results;

namespace SinePulse.EMS.Messages.BillingMessages
{
  public class AddBillingDataResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public AddBillingDataResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}