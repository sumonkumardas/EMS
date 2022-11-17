using FluentValidation;
using SinePulse.EMS.Messages.ServiceChargeMessages;

namespace SinePulse.EMS.UseCases.ServiceCharges
{
  public class GetServiceChargeRequestMessageValidator : AbstractValidator<GetServiceChargeRequestMessage>
  {
    public GetServiceChargeRequestMessageValidator()
    {

    }
  }
}
