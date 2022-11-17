using FluentValidation;
using SinePulse.EMS.Messages.OnlinePaymentMessages;

namespace SinePulse.EMS.UseCases.OnlinePayments
{
  public class DeleteFailedTransactionRequestMessageValidator : AbstractValidator<DeleteFailedTransactionRequestMessage>
  {
    public DeleteFailedTransactionRequestMessageValidator()
    {
    }
  }
}