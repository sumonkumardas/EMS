using FluentValidation;
using SinePulse.EMS.Messages.SalaryComponentTypeMessage;

namespace SinePulse.EMS.UseCases.SalaryComponentTypes
{
  public class ShowSalaryComponentTypeListRequestMessageValidator : AbstractValidator<ShowSalaryComponentTypeListRequestMessage>
  {
    public ShowSalaryComponentTypeListRequestMessageValidator()
    {

    }
  }
}
