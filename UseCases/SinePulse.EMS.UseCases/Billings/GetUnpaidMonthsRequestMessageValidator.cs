using FluentValidation;
using SinePulse.EMS.Messages.BillingMessages;

namespace SinePulse.EMS.UseCases.Billings
{
  public class GetUnpaidMonthsRequestMessageValidator : AbstractValidator<GetUnpaidMonthsRequestMessage>
  {
    public GetUnpaidMonthsRequestMessageValidator()
    {
    }
  }
}