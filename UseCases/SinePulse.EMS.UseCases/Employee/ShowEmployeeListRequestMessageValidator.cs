using FluentValidation;
using SinePulse.EMS.Messages.EmployeeMessages;

namespace SinePulse.EMS.UseCases.Employee
{
  public class ShowEmployeeListRequestMessageValidator : AbstractValidator<ShowEmployeeListRequestMessage>
  {

    public ShowEmployeeListRequestMessageValidator()
    {
    }
    
  }
}