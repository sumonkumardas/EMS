using FluentValidation;
using SinePulse.EMS.Messages.BillingMessages;

namespace SinePulse.EMS.UseCases.Billings
{
  public class GetDueAmountRequestMessageValidator : AbstractValidator<GetDueAmountRequestMessage>
  {
    public GetDueAmountRequestMessageValidator()
    {
    }
  }
}