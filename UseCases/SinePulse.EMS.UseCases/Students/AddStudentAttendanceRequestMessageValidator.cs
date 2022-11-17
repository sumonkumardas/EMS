using FluentValidation;
using SinePulse.EMS.Messages.AttendanceMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public class AddStudentAttendanceRequestMessageValidator : AbstractValidator<AddStudentAttendanceRequestMessage>
  {
    public AddStudentAttendanceRequestMessageValidator()
    {
      RuleFor(x => x.AttendanceDate).NotEmpty().WithMessage("Please specify Attendance Date");
      RuleFor(x => x.AttendanceDate).NotNull().WithMessage("Please specify Attendance Date");

      RuleFor(x => x.InTime).NotEmpty().WithMessage("Please specify In Time");
      RuleFor(x => x.InTime).NotNull().WithMessage("Please specify In Time");
      

      RuleFor(x => x.OutTime).NotEmpty().WithMessage("Please specify Out Time");
      RuleFor(x => x.OutTime).NotNull().WithMessage("Please specify Out Time");

      RuleFor(x => x.OutTime).GreaterThan(p => p.InTime).WithMessage("Out Time must be less than In Time.");
      
    }
  }
}