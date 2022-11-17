using FluentValidation;
using SinePulse.EMS.Messages.AttendanceMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public class ShowStudentAttendanceRequestMessageValidator : AbstractValidator<ShowStudentAttendanceRequestMessage>
  {
    public ShowStudentAttendanceRequestMessageValidator()
    {
    }
  }
}