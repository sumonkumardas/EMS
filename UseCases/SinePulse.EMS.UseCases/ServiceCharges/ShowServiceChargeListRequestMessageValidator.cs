using FluentValidation;
using SinePulse.EMS.Messages.ServiceChargeMessages;

namespace SinePulse.EMS.UseCases.ServiceCharges
{
  public class ShowServiceChargeListRequestMessageValidator : AbstractValidator<ShowServiceChargeListRequestMessage>
  {
    public ShowServiceChargeListRequestMessageValidator()
    {

    }
  }
}
