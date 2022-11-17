using FluentValidation;
using SinePulse.EMS.Messages.SalaryComponentMessages;

namespace SinePulse.EMS.UseCases.SalaryComponents
{
  public class ShowSalaryComponentListRequestMessageValidator : AbstractValidator<ShowSalaryComponentListRequestMessage>
  {
    public ShowSalaryComponentListRequestMessageValidator()
    {

    }
  }
}
