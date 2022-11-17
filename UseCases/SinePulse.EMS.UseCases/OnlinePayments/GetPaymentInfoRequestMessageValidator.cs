using FluentValidation;
using SinePulse.EMS.Messages.OnlinePaymentMessages;

namespace SinePulse.EMS.UseCases.OnlinePayments
{
  public class GetPaymentInfoRequestMessageValidator : AbstractValidator<GetPaymentInfoRequestMessage>
  {
    public GetPaymentInfoRequestMessageValidator()
    {
    }
  }
}