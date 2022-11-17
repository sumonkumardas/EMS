using FluentValidation;
using SinePulse.EMS.Messages.ServiceChargeMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.UseCases.ServiceCharges
{
  public class DisplayEditServiceChargeRequestMessageValidator : AbstractValidator<DisplayEditServiceChargeRequestMessage>
  {
    public DisplayEditServiceChargeRequestMessageValidator()
    {

    }
  }
}
