using FluentValidation.Results;

namespace SinePulse.EMS.Messages.OnlinePaymentMessages
{
  public class UpdateTransactionInfoResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public UpdateTransactionInfoResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}