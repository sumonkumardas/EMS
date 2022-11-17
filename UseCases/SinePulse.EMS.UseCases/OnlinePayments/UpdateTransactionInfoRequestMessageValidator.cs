using FluentValidation;
using SinePulse.EMS.Messages.OnlinePaymentMessages;

namespace SinePulse.EMS.UseCases.OnlinePayments
{
  public class UpdateTransactionInfoRequestMessageValidator : AbstractValidator<UpdateTransactionInfoRequestMessage>
  {
    public UpdateTransactionInfoRequestMessageValidator()
    {
    }
  }
}