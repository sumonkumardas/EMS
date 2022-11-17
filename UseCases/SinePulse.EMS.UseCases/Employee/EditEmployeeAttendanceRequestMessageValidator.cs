using FluentValidation;
using SinePulse.EMS.Messages.AttendanceMessages;
using System;

namespace SinePulse.EMS.UseCases.Employee
{
  public class EditEmployeeAttendanceRequestMessageValidator : AbstractValidator<EditEmployeeAttendanceRequestMessage>
  {
    public EditEmployeeAttendanceRequestMessageValidator()
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