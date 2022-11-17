using FluentValidation.Results;

namespace SinePulse.EMS.Messages.OnlinePaymentMessages
{
  public class DeleteFailedTransactionResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public DeleteFailedTransactionResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}