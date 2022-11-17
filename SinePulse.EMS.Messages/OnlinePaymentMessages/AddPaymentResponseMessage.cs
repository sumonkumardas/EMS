using FluentValidation.Results;

namespace SinePulse.EMS.Messages.OnlinePaymentMessages
{
  public class AddPaymentResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public AddPaymentResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}