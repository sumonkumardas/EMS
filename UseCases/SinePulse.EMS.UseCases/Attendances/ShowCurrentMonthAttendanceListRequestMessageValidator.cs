using FluentValidation;
using SinePulse.EMS.Messages.AttendanceMessages;

namespace SinePulse.EMS.UseCases.Attendances
{
  public class
    ShowCurrentMonthAttendanceListRequestMessageValidator : AbstractValidator<ShowCurrentMonthAttendanceListRequestMessage>
  {
    public ShowCurrentMonthAttendanceListRequestMessageValidator()
    {
    }
  }
}