using FluentValidation;
using SinePulse.EMS.Messages.BillingMessages;

namespace SinePulse.EMS.UseCases.Billings
{
  public class GetBillingDataByYearRequestMessageValidator : AbstractValidator<GetBillingDataByYearRequestMessage>
  {
  }
}