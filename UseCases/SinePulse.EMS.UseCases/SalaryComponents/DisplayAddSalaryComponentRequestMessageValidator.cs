using FluentValidation;
using SinePulse.EMS.Messages.SalaryComponentMessages;

namespace SinePulse.EMS.UseCases.SalaryComponents
{
  public class DisplayAddSalaryComponentRequestMessageValidator : AbstractValidator<DisplayAddSalaryComponentRequestMessage>
  {
    public DisplayAddSalaryComponentRequestMessageValidator()
    {

    }
  }
}
