using FluentValidation;
using SinePulse.EMS.Messages.OnlinePaymentMessages;

namespace SinePulse.EMS.UseCases.OnlinePayments
{
  public class GetUserInfoRequestMessageValidator : AbstractValidator<GetUserInfoRequestMessage>
  {
    public GetUserInfoRequestMessageValidator()
    {
    }
  }
}