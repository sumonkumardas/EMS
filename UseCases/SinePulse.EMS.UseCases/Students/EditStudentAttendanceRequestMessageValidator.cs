using FluentValidation;
using SinePulse.EMS.Messages.AttendanceMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public class EditStudentAttendanceRequestMessageValidator : AbstractValidator<EditStudentAttendanceRequestMessage>
  {
    public EditStudentAttendanceRequestMessageValidator()
    {
      RuleFor(x => x.AttendanceDate).NotEmpty().WithMessage("Please specify Attendance Date");
      RuleFor(x => x.AttendanceDate).NotNull().WithMessage("Please specify Attendance Date");

      RuleFor(x => x.InTime).NotEmpty().WithMessage("Please specify In Time");
      RuleFor(x => x.InTime).NotNull().WithMessage("Please specify In Time");
      

      RuleFor(x => x.OutTime).NotEmpty().WithMessage("Please specify Out Time");
      RuleFor(x => x.OutTime).NotNull().WithMessage("Please specify Out Time");

      RuleFor(x => x.OutTime).GreaterThan(p => p.InTime).WithMessage("Out Time must be Greater than In Time.");
      
    }
  }
}