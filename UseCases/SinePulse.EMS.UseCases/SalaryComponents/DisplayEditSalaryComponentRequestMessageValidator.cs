using FluentValidation;
using SinePulse.EMS.Messages.SalaryComponentMessages;

namespace SinePulse.EMS.UseCases.SalaryComponents
{
  public class DisplayEditSalaryComponentRequestMessageValidator : AbstractValidator<DisplayEditSalaryComponentRequestMessage>
  {
    public DisplayEditSalaryComponentRequestMessageValidator()
    {

    }
  }
}
