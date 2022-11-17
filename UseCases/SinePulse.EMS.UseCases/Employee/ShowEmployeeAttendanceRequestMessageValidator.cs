using FluentValidation;
using SinePulse.EMS.Messages.AttendanceMessages;

namespace SinePulse.EMS.UseCases.Employee
{
  public class ShowEmployeeAttendanceRequestMessageValidator : AbstractValidator<ShowEmployeeAttendanceRequestMessage>
  {

    public ShowEmployeeAttendanceRequestMessageValidator()
    {
      RuleFor(x => x.AttendanceId).NotEmpty().WithMessage("Invalid Id.");
      RuleFor(x => x.AttendanceId).GreaterThan(0).WithMessage("Invalid Id.");
    }
    
  }
}