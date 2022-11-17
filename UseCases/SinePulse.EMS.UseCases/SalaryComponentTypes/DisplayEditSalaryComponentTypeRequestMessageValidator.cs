using FluentValidation;
using SinePulse.EMS.Messages.SalaryComponentTypeMessage;

namespace SinePulse.EMS.UseCases.SalaryComponentTypes
{
  public class DisplayEditSalaryComponentTypeRequestMessageValidator : AbstractValidator<DisplayEditSalaryComponentTypeRequestMessage>
  {
    public DisplayEditSalaryComponentTypeRequestMessageValidator()
    {

    }
  }
}
