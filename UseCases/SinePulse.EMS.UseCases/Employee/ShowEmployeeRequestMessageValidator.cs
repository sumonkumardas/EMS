using FluentValidation;
using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Employee
{
  public class ShowEmployeeRequestMessageValidator : AbstractValidator<ShowEmployeeRequestMessage>
  {

    public ShowEmployeeRequestMessageValidator()
    {
      RuleFor(x => x.EmployeeId).NotEmpty().WithMessage("Invalid Id.");
      RuleFor(x => x.EmployeeId).GreaterThan(0).WithMessage("Invalid Id.");
    }
    
  }
}